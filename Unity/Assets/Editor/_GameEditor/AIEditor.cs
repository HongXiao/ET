// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Reflection;
// using ET.AI;
// using ET.AI.Node;
// using UnityEditor;
// using UnityEngine;
//
// namespace ET._GameEditor
// {
//     public class AIEditor: EditorWindow
//     {
//         private const string ConfigFile = @"..\Tools\cwRsync\Config\AiConfig.json";
//         private AiConfigCategory aiConfigCategory;
//         private AiConfig editorAiConfig;
//         private List<int> Ids;
//         private int SelectIndex = 0;
//         private int newConfigId = 1000;
//         private List<Type> AiConditions = new List<Type>();
//         private List<Type> AiNodes = new List<Type>();
//         
//         [MenuItem("Tools/AI编辑器")]
//         private static void ShowWindow()
//         {
//             GetWindow(typeof (AIEditor), false, "AI编辑器");
//         }
//         private void OnEnable()
//         {
//             this.InitAiConfig();
//         }
//         public void OnGUI()
//         {
//             this.SelectAiCongig();
//             this.EditorAiConfig();
//         }
//         private void InitAiConfig()
//         {
//             if (!File.Exists(ConfigFile))
//             {
//                 this.aiConfigCategory = new AiConfigCategory();
//             }
//             else
//             {
//                 string s = File.ReadAllText(ConfigFile);
//                 this.aiConfigCategory = MongoHelper.FromJson<AiConfigCategory>(s);
//             }
//             var allAi = this.aiConfigCategory.GetAll().Values.ToList();
//             this.Ids = allAi.ConvertAll(p => p.Id);
//             allAi.ForEach(p =>
//             {
//                 if (p.Id > this.newConfigId) this.newConfigId = p.Id;
//             });
//             
//             string[] assemblyNames = { "Unity.Model.dll", "Unity.Hotfix.dll", "Unity.ModelView.dll", "Unity.HotfixView.dll" };
//             foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
//             {
//                 string assemblyName = assembly.ManifestModule.Name;
//                 if (!assemblyNames.Contains(assemblyName))
//                     continue;
//                 Game.EventSystem.Add(assembly);	
//             }
//             var types = Game.EventSystem.GetTypes(typeof (AiHandlerAttribute));
//             Log.Debug("Ai Type Count:"+types.Count);
//             foreach (Type type in types)
//             {
//                 object ai = Activator.CreateInstance(type) as AiCondition;
//                 if (ai == null)
//                 {
//                     Log.Error("Ai Type 必须继承 AiCondition Error Type:"+ type.Name);
//                     continue;
//                 }
//                 Log.Debug("find ai Type:"+type.Name);
//                 if (ai is AiHandler aiNode)
//                     this.AiNodes.Add(type);
//                 else
//                     this.AiConditions.Add(type);
//             }
//         }
//         private void SelectAiCongig()
//         {
//             if (this.editorAiConfig != null)
//                 return;
//             GUILayout.Label($"当前有AI配置{this.Ids.Count}条");
//             this.newConfigId = EditorGUILayout.IntField("请输入新加AI配置Id", this.newConfigId);
//             if (GUILayout.Button("添加一条AI配置"))
//             {
//                 if (this.aiConfigCategory.Contain(this.newConfigId))
//                 {
//                     Log.Error($"新加Id已经存在");
//                     EditorUtility.DisplayDialog("AI编辑错误", $"新加Id:{this.newConfigId}已经存在,重新输入!", "确定");
//                 }
//                 else
//                 {
//                     var aiConfig = new AiConfig() { Id = this.newConfigId };
//                     var map = this.aiConfigCategory.GetAll();
//                     map.Add(this.newConfigId, aiConfig);
//                     this.Ids.Add(this.newConfigId);
//                     this.SelectIndex = this.Ids.Count - 1;
//                     this.newConfigId++;
//                 }
//             }
//             if (this.Ids.Count != 0)
//             {
//                 this.SelectIndex = EditorGUILayout.Popup("选择AI配置", this.SelectIndex, this.Ids.ConvertAll(p => p.ToString()).ToArray());
//                 int selectId = this.Ids[this.SelectIndex];
//                 if (GUILayout.Button($"编辑AI:{selectId}"))
//                 {
//                     this.editorAiConfig = this.aiConfigCategory.Get(selectId);
//                 }
//                 if (GUILayout.Button($"删除AI:{selectId}"))
//                 {
//                     this.Ids.Remove(selectId);
//                     var all = this.aiConfigCategory.GetAll();
//                     all.Remove(selectId);
//                     if (this.SelectIndex >= this.Ids.Count)
//                         this.SelectIndex = this.Ids.Count - 1;
//                 }
//                 if (GUILayout.Button("保存AI"))
//                 {
//                 }
//             }
//         }
//
//         private int NodeInde;
//         private void EditorAiConfig()
//         {
//             if (this.editorAiConfig == null)
//                 return;
//             GUILayout.Label($"当前正在编辑AI配置Id:{this.editorAiConfig.Id}");
//             if (GUILayout.Button("返回"))
//             {
//                 this.editorAiConfig = null;
//             }
//             GUILayout.Label($"Has AiNode Count:{this.editorAiConfig.Nodes.Count}");
//             foreach (Type type in this.AiNodes)
//                 if (EditorAiNode(type))
//                     break;
//         }
//
//         private bool EditorAiNode(Type type)
//         {
//             var find = this.editorAiConfig.Nodes.Find(p => p.GetType() == type);
//             if (find != null)
//             {
//                 GUILayout.BeginHorizontal();
//                 GUILayout.Label($"{type.Name}");
//                 if (GUILayout.Button($"Remove Ai Node:{type.Name}"))
//                 {
//                     this.editorAiConfig.Nodes.Remove(find);
//                     return true;
//                 }
//                 GUILayout.EndHorizontal();
//                 string newJson = GUILayout.TextField(JsonHelper.ToJson(find));
//                 this.EditorAiCondition(find.Conditions);
//             }
//             else
//             {
//                 if (GUILayout.Button($"Add Ai Node:{type.Name}"))
//                 {
//                     AiHandler aiHandler = Activator.CreateInstance(type) as AiHandler;
//                     this.editorAiConfig.Nodes.Add(aiHandler);
//                     return true;
//                 }
//             }
//             GUILayout.Space(10);
//             return false;
//         }
//
//         private bool EditorAiCondition(List<AiCondition> conditions)
//         {
//             foreach (Type type in this.AiConditions)
//             {
//                 var find = conditions.Find(p => p.GetType() == type);
//                 if (find != null)
//                 {
//                     GUILayout.BeginHorizontal();
//                     GUILayout.Label($"{type.Name}");
//                     if (GUILayout.Button($"Remove Ai Condition:{type.Name}"))
//                     {
//                         conditions.Remove(find);
//                         return true;
//                     }
//                     GUILayout.EndHorizontal();
//                     string newJson = GUILayout.TextField(JsonHelper.ToJson(find));
//                     this.EditorAiCondition(find.Conditions);
//                 }
//                 else
//                 {
//                     if (GUILayout.Button($"Add Ai Condition:{type.Name}"))
//                     {
//                         AiCondition condition = Activator.CreateInstance(type) as AiCondition;
//                         conditions.Add(condition);
//                         return true;
//                     }
//                 }
//                 GUILayout.Space(5);
//             }
//             return false;
//         }
//     }
// }