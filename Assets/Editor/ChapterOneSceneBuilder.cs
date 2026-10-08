using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Eoduk.Core;
using Eoduk.Core.Config;
using Eoduk.Player;
using Eoduk.Systems;
using Eoduk.Gaze;
using Eoduk.Enemies;
using Eoduk.Sequences;
using Eoduk.UI;

namespace Eoduk.EditorTools
{
    public static class ChapterOneSceneBuilder
    {
        private const string Root = "Assets/";
        private static readonly Dictionary<string, Material> Materials = new Dictionary<string, Material>();

        [MenuItem("Eoduk/Build Chapter 1 Scenes")]
        public static void BuildAll()
        {
            EnsureFolders();
            var playerConfig = Config<PlayerConfig>("Assets/Data/Config/CFG_Player.asset");
            var beadConfig = Config<BeadConfig>("Assets/Data/Config/CFG_Bead.asset");
            var camConfig = Config<CamcorderConfig>("Assets/Data/Config/CFG_Camcorder.asset");
            var eyeConfig = Config<EyeCloseConfig>("Assets/Data/Config/CFG_EyeClose.asset");
            var interactionConfig = Config<InteractionConfig>("Assets/Data/Config/CFG_Interaction.asset");
            var gazeProfile = Config<GazeProfile>("Assets/Data/Gaze/GAZE_Ch1.asset");
            BuildDialogueTables();
            BuildBoot();
            BuildIntro(playerConfig, beadConfig, camConfig, eyeConfig, interactionConfig, gazeProfile);
            BuildWell(playerConfig, beadConfig, camConfig, eyeConfig, interactionConfig, gazeProfile);
            BuildComplete();
            var scenes = new List<EditorBuildSettingsScene>
            {
                new EditorBuildSettingsScene("Assets/Scenes/Boot.unity", true),
                new EditorBuildSettingsScene("Assets/Scenes/Ch1_Intro.unity", true),
                new EditorBuildSettingsScene("Assets/Scenes/Ch1_Well.unity", true),
                new EditorBuildSettingsScene("Assets/Scenes/Ch1_Complete.unity", true)
            };
            EditorBuildSettings.scenes = scenes.ToArray();
            AssetDatabase.SaveAssets(); AssetDatabase.Refresh();
            Debug.Log("Eoduk Chapter 1 scenes, configuration and dialogue assets created.");
        }

        private static void EnsureFolders()
        {
            string[] folders = { "Assets/Editor", "Assets/Data/Config", "Assets/Data/Gaze", "Assets/Resources", "Assets/Resources/Dialogue", "Assets/Scenes", "Assets/Prefabs", "Assets/Prefabs/Player", "Assets/Docs", "Assets/Art/Materials" };
            foreach (var folder in folders) if (!AssetDatabase.IsValidFolder(folder))
            {
                var parent = System.IO.Path.GetDirectoryName(folder).Replace('\\', '/');
                AssetDatabase.CreateFolder(parent, System.IO.Path.GetFileName(folder));
            }
        }

        private static T Config<T>(string path) where T : ScriptableObject
        {
            var value = AssetDatabase.LoadAssetAtPath<T>(path);
            if (value != null) return value;
            value = ScriptableObject.CreateInstance<T>(); AssetDatabase.CreateAsset(value, path); return value;
        }

        private static void BuildDialogueTables()
        {
            SaveDialogue("DLG_Ch1_Intro", new[] {
                new DialogueLine { speaker="나", text="밤의 우물은 어릴 적 기억과 다르게 깊고 낯설었다.", seconds=3f },
                new DialogueLine { speaker="캠코더", text="LCD 화면 안쪽에만, 물속에서 올려다보는 얼굴이 비쳤다.", seconds=3.4f },
                new DialogueLine { speaker="나", text="화면을 끄자 손가락이 손목을 잡아당겼다. 녹화 시각은 거꾸로 흐르기 시작했다.", seconds=4f }
            });
            SaveDialogue("DLG_Ch1_GumiIntro", new[] {
                new DialogueLine { speaker="구미", text="여긴 1672년이야. 네가 가진 구슬을 놓치지 마.", seconds=3.4f },
                new DialogueLine { speaker="구미", text="마을을 지나 다시 우물로 와. 그리고… 부르기 전에는 돌아보지 마.", seconds=4f }
            });
            SaveDialogue("DLG_Ch1_HatClue", new[] {
                new DialogueLine { speaker="나", text="빈 항아리가 바닥을 긁는다. 캠코더에만 작은 갓 그림자가 남았다.", seconds=3.2f }
            });
            SaveDialogue("DLG_Ch1_EmptyJar", new[] {
                new DialogueLine { speaker="나", text="마을 안쪽에서 빈 항아리가 바닥을 긁는 소리가 난다. 누군가 방금 지나간 흔적이다.", seconds=3.6f }
            });
            SaveDialogue("DLG_Ch1_Eoduksini", new[] {
                new DialogueLine { speaker="나", text="내가 바라보는 동안, 갓을 쓴 그림자가 한 걸음씩 가까워졌다.", seconds=3.4f }
            });
            SaveDialogue("DLG_Ch1_ReturnWell", new[] {
                new DialogueLine { speaker="구미", text="돌아보지 마. 내 목소리가 끝날 때까지 우물만 봐.", seconds=4f }
            });
        }

        private static void BuildBoot()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            SetupWorld("Boot", Color.black, 0);
            AddStaticCamera("Boot Camera", new Vector3(0, 0, -10));
            var root = new GameObject("Boot Loader"); root.AddComponent<BootLoader>();
            var canvas = new GameObject("Boot Splash", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)); canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var title = canvas.AddComponent<Text>(); title.font = Font.CreateDynamicFontFromOSFont("Arial", 32); title.fontSize = 32; title.color = new Color(.9f,.89f,.82f); title.alignment = TextAnchor.MiddleCenter; title.text = "어둑\n\n우물의 기억";
            SaveScene(scene, "Assets/Scenes/Boot.unity");
        }

        private static void SaveDialogue(string id, DialogueLine[] lines)
        {
            string path = "Assets/Resources/Dialogue/" + id + ".asset";
            var table = AssetDatabase.LoadAssetAtPath<DialogueTable>(path);
            if (table == null)
            {
                if (AssetDatabase.LoadMainAssetAtPath(path) != null) AssetDatabase.DeleteAsset(path);
                table = ScriptableObject.CreateInstance<DialogueTable>(); AssetDatabase.CreateAsset(table, path);
            }
            table.id = id; table.lines = new List<DialogueLine>(lines); EditorUtility.SetDirty(table);
        }

        private static void BuildIntro(PlayerConfig p, BeadConfig b, CamcorderConfig c, EyeCloseConfig e, InteractionConfig i, GazeProfile g)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            SetupWorld("현대의 우물", new Color(.008f, .012f, .022f), 0.018f);
            Ground(new Vector3(0, -.12f, 0), new Vector3(80, .2f, 80), "Wet Ground", "soil");
            WellProp(new Vector3(0, 0, 8), "현대의 우물");
            var player = CreatePlayer(new Vector3(0, 0, 1), p, b, c, e, i, g, true);
            var gumiModel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/Characters/SK_Gumi.fbx");
            if (gumiModel != null)
            {
                var face = (GameObject)PrefabUtility.InstantiatePrefab(gumiModel); face.name = "LCD Only Gumi";
                face.transform.SetPositionAndRotation(new Vector3(0, 0, 6.5f), Quaternion.Euler(90, 180, 0));
                ApplyCharacterMaterials(face, true);
                SetLayerRecursively(face, 2);
            }
            var opening = player.AddComponent<DialogueOnStart>(); Set(opening, "dialogueId", "DLG_Ch1_Intro");
            var timeLeap = new GameObject("Time Leap Sequence").AddComponent<TimeLeapSequence>();
            Set(timeLeap, "targetScene", "Ch1_Well");
            var trigger = Primitive("우물 조사 지점", PrimitiveType.Cube, new Vector3(0, 1.2f, 7f), new Vector3(3, 2.4f, 1f), "stone", false);
            var collider = trigger.AddComponent<BoxCollider>(); collider.isTrigger = true;
            var sequence = trigger.AddComponent<SequenceInteractable>(); Set(sequence, "prompt", "우물 안을 들여다본다"); Set(sequence, "timeLeap", timeLeap);
            AddCheckpoint("현대 우물", new Vector3(0, 0, -.5f), "ch1_intro");
            var dialogue = MakeDialogueCanvas();
            var d = dialogue.AddComponent<DialogueUI>(); Set(d, "speakerText", dialogue.transform.Find("Speaker").GetComponent<Text>()); Set(d, "bodyText", dialogue.transform.Find("Body").GetComponent<Text>()); Set(d, "group", dialogue.GetComponent<CanvasGroup>());
            var fade = MakeFadeCanvas(); Set(timeLeap, "fade", fade.GetComponent<CanvasGroup>());
            MakeControlsHint();
            SaveScene(scene, "Assets/Scenes/Ch1_Intro.unity");
            CreatePlayerPrefab(player);
        }

        private static void BuildWell(PlayerConfig p, BeadConfig b, CamcorderConfig c, EyeCloseConfig e, InteractionConfig i, GazeProfile g)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            SetupWorld("1672년 버려진 마을", new Color(.014f, .020f, .026f), .022f);
            var envPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/Environment/Ch1/SM_Ch1_WellVillage.fbx");
            if (envPrefab != null)
            {
                var env = (GameObject)PrefabUtility.InstantiatePrefab(envPrefab);
                env.name = "SM_Ch1_WellVillage"; env.transform.rotation = Quaternion.Euler(90, 0, 0);
                foreach (var mf in env.GetComponentsInChildren<MeshFilter>(true))
                    if (mf.sharedMesh != null && mf.GetComponent<Collider>() == null) { var mc = mf.gameObject.AddComponent<MeshCollider>(); mc.sharedMesh = mf.sharedMesh; }
                foreach (var renderer in env.GetComponentsInChildren<Renderer>(true))
                {
                    string part = renderer.name.ToLowerInvariant();
                    renderer.sharedMaterial = part.Contains("water") || part.Contains("innerdark") ? Material("water") :
                        part.Contains("rope") || part.Contains("wood") || part.Contains("door") || part.Contains("bucket") || part.Contains("tree") || part.Contains("axle") || part.Contains("post") ? Material("wood") :
                        part.Contains("grass") ? Material("foliage") : part.Contains("roof") ? Material("roof") :
                        part.Contains("gold") || part.Contains("medallion") ? Material("brass") :
                        part.Contains("robe") || part.Contains("hat") || part.Contains("veil") ? Material("black") :
                        part.Contains("bead") ? Material("bead") : part.Contains("fox") || part.Contains("tail") ? Material("ghost") : Material("stone");
                }
            }
            Ground(new Vector3(0, -.14f, 0), new Vector3(150, .25f, 150), "Extended Village Ground", "soil");
            var player = CreatePlayer(new Vector3(0, 0, -7), p, b, c, e, i, g, false);
            var gumiModel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/Characters/SK_Gumi.fbx");
            GameObject gumi = gumiModel != null ? (GameObject)PrefabUtility.InstantiatePrefab(gumiModel) : Primitive("Gumi", PrimitiveType.Capsule, new Vector3(-2, 0, -3), Vector3.one, "ghost", false);
            gumi.name = "Gumi Guide"; gumi.transform.SetPositionAndRotation(new Vector3(-2, 0, -3), Quaternion.Euler(90, 0, 0));
            ApplyCharacterMaterials(gumi, true);
            var guide = gumi.AddComponent<GumiGuide>(); Set(guide, "dialogueId", "DLG_Ch1_GumiIntro");
            var beadPickup = Primitive("구슬의 잔광", PrimitiveType.Sphere, new Vector3(-2, 1.1f, -3), Vector3.one * .22f, "bead", false);
            var handover = gumi.AddComponent<BeadHandoverSequence>(); Set(handover, "bead", player.GetComponentInChildren<BeadLight>()); Set(handover, "pickup", beadPickup);
            Set(guide, "handover", handover); Set(guide, "beadPickup", beadPickup);
            var gumiInteract = Trigger("구미와 대화", new Vector3(-2, 1, -2), new Vector3(2, 2, 1));
            var gi = gumiInteract.AddComponent<SequenceInteractable>(); Set(gi, "prompt", "구미에게 다가가 구슬을 받는다"); Set(gi, "gumi", guide);
            AddCheckpoint("Ch1_Checkpoint_01", new Vector3(0, 0, -7), "ch1_well_start");
            AddCheckpoint("Ch1_Checkpoint_02", new Vector3(0, 0, 8), "ch1_village_mid");
            AddCheckpoint("Ch1_Checkpoint_03", new Vector3(0, 0, 16), "ch1_village_far");
            AddCheckpoint("Ch1_Checkpoint_04", new Vector3(0, 0, -3.5f), "ch1_return_well");
            var eodukModel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/Characters/SK_Eoduksini.fbx");
            GameObject eoduk = eodukModel != null ? (GameObject)PrefabUtility.InstantiatePrefab(eodukModel) : Primitive("Eoduksini", PrimitiveType.Capsule, new Vector3(0, 0, 8), Vector3.one, "black", false);
            eoduk.name = "Eoduksini"; eoduk.transform.SetPositionAndRotation(new Vector3(0, 0, 12), Quaternion.Euler(90, 0, 0));
            ApplyCharacterMaterials(eoduk, false);
            var ai = eoduk.AddComponent<EoduksiniAI>(); eoduk.SetActive(false); eoduk.SetActive(true);
            var gaze = player.AddComponent<GazeSystem>(); Set(gaze, "viewCamera", player.GetComponentInChildren<Camera>()); Set(gaze, "camcorder", player.GetComponentInChildren<CamcorderController>()); Set(gaze, "eyes", player.GetComponentInChildren<EyeCloseController>()); Set(gaze, "profile", g);
            var warning = player.GetComponentInChildren<BeadLight>().gameObject.AddComponent<BeadWarning>(); Set(warning, "config", b); Set(warning, "bead", player.GetComponentInChildren<Light>()); Set(warning, "threat", eoduk.transform);
            var flowRoot = new GameObject("Chapter 1 Flow"); var flow = flowRoot.AddComponent<ChapterOneFlow>(); Set(flow, "player", player.transform); Set(flow, "eoduksini", ai);
            var spawn = Empty("Eoduksini Spawn", new Vector3(0, 0, 0)); Set(flow, "spawnPoint", spawn.transform); Set(flow, "spawnDistance", 17f);
            var ret = Empty("Return Well", new Vector3(0, 0, -3.5f)); Set(flow, "returnPoint", ret.transform);
            var apparition = Empty("Mabapae Apparition", new Vector3(0, 0, -6.5f)); Set(flow, "apparitionPoint", apparition.transform);
            var finale = new GameObject("Final Turn").AddComponent<FinalTurnSequence>(); Set(finale, "player", player.transform); Set(finale, "apparition", apparition.transform);
            Set(flow, "finale", finale);
            var fade = MakeFadeCanvas(); Set(finale, "fade", fade.GetComponent<CanvasGroup>());
            var apparitionModel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/Characters/SK_Eoduksini.fbx");
            if (apparitionModel != null)
            {
                var ghost = (GameObject)PrefabUtility.InstantiatePrefab(apparitionModel); ghost.name = "Mabapae Reveal"; ghost.transform.SetParent(apparition.transform, false); ghost.transform.localPosition = Vector3.zero; ghost.transform.localRotation = Quaternion.Euler(90, 0, 0); ghost.transform.localScale = Vector3.one * 1.15f;
                ApplyCharacterMaterials(ghost, false);
            }
            var chaseTrigger = Trigger("First Encounter", new Vector3(0, 0, 17), new Vector3(6, 3, 2));
            chaseTrigger.AddComponent<ChapterOneTrigger>().Initialize(flow, ChapterOneTrigger.Kind.Chase);
            var returnTrigger = Trigger("Return Route", new Vector3(0, 0, 22), new Vector3(8, 3, 3)); returnTrigger.AddComponent<ChapterOneTrigger>().Initialize(flow, ChapterOneTrigger.Kind.Return);
            Primitive("Gap Left Wall", PrimitiveType.Cube, new Vector3(-2.15f, 1.5f, 5), new Vector3(.35f, 3f, 14f), "stone", true);
            Primitive("Gap Right Wall", PrimitiveType.Cube, new Vector3(2.15f, 1.5f, 5), new Vector3(.35f, 3f, 14f), "stone", true);
            Primitive("Crouch Gap Lintel", PrimitiveType.Cube, new Vector3(0, 1.52f, 5), new Vector3(4.65f, .35f, 3f), "stone", true);
            var jarBeat = Trigger("Empty Jar Scrape", new Vector3(0, 0, 12), new Vector3(5, 3, 3)); var jar = jarBeat.AddComponent<PacingBeat>(); Set(jar, "dialogueId", "DLG_Ch1_EmptyJar");
            var hatRoot = Empty("LCD Only Hat Silhouette", new Vector3(7, 1.5f, 9));
            var brim = Primitive("Hat Brim", PrimitiveType.Cylinder, hatRoot.transform.position, new Vector3(.55f, .06f, .55f), "black", false); brim.transform.SetParent(hatRoot.transform, true);
            var crown = Primitive("Hat Crown", PrimitiveType.Cylinder, hatRoot.transform.position + Vector3.up * .16f, new Vector3(.28f, .16f, .28f), "black", false); crown.transform.SetParent(hatRoot.transform, true);
            SetLayerRecursively(hatRoot, 2); hatRoot.SetActive(false);
            var foreshadow = Trigger("Foreshadow", new Vector3(7, 0, 9), new Vector3(3, 3, 3)).AddComponent<ForeshadowTrigger>(); Set(foreshadow, "silhouette", hatRoot);
            var dialogue = MakeDialogueCanvas(); var d = dialogue.AddComponent<DialogueUI>(); Set(d, "speakerText", dialogue.transform.Find("Speaker").GetComponent<Text>()); Set(d, "bodyText", dialogue.transform.Find("Body").GetComponent<Text>()); Set(d, "group", dialogue.GetComponent<CanvasGroup>());
            var pause = dialogue.AddComponent<PauseMenu>();
            var pausePanel = MakePausePanel(); Set(pause, "panel", pausePanel);
            MakeControlsHint();
            var interaction = player.GetComponent<InteractionSystem>(); var prompt = MakePromptCanvas(); var pui = prompt.AddComponent<InteractionPromptUI>(); Set(pui, "interaction", interaction); Set(pui, "label", prompt.GetComponent<Text>());
            var death = flowRoot.AddComponent<DeathHandler>(); Set(death, "player", player.transform);
            flowRoot.AddComponent<WindSystem>();
            SaveScene(scene, "Assets/Scenes/Ch1_Well.unity");
        }

        private static void BuildComplete()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            SetupWorld("Chapter 1 Complete", Color.black, 0);
            AddStaticCamera("Ending Camera", new Vector3(0, 0, -10));
            var canvas = MakeDialogueCanvas(); var body = canvas.transform.Find("Body").GetComponent<Text>(); body.alignment = TextAnchor.MiddleCenter; body.fontSize = 30; body.text = "CHAPTER 1\n\n우물 아래에서 기다리던 것은, 구미가 아니었다.\n\n[ESC] 종료";
            canvas.transform.Find("Speaker").GetComponent<Text>().text = "어둑";
            canvas.GetComponent<CanvasGroup>().alpha = 1;
            SaveScene(scene, "Assets/Scenes/Ch1_Complete.unity");
        }

        private static GameObject CreatePlayer(Vector3 position, PlayerConfig p, BeadConfig b, CamcorderConfig c, EyeCloseConfig e, InteractionConfig i, GazeProfile g, bool includeTransition)
        {
            var player = new GameObject("Player"); player.tag = "Player"; player.transform.position = position;
            var cc = player.AddComponent<CharacterController>(); cc.height = p.colliderHeight; cc.radius = .3f; cc.center = Vector3.up * .9f; cc.stepOffset = .25f; cc.slopeLimit = 45;
            var input = player.AddComponent<InputRouterBootstrap>(); Set(input, "actions", AssetDatabase.LoadAssetAtPath<InputActionAsset>("Assets/Input/GameInput.inputactions"));
            var pivot = new GameObject("Camera Pivot").transform; pivot.SetParent(player.transform, false); pivot.localPosition = Vector3.up * p.cameraHeight;
            var cameraGo = new GameObject("Player Camera"); cameraGo.tag = "MainCamera"; cameraGo.transform.SetParent(pivot, false); cameraGo.transform.localPosition = Vector3.zero;
            var cam = cameraGo.AddComponent<Camera>(); cam.nearClipPlane = .03f; cam.farClipPlane = 80; cam.fieldOfView = 70;
            cam.clearFlags = CameraClearFlags.SolidColor; cam.backgroundColor = new Color(.012f, .02f, .027f);
            cameraGo.AddComponent<AudioListener>();
            var pc = player.AddComponent<PlayerController>(); Set(pc, "config", p); Set(pc, "cameraPivot", pivot);
            var eye = player.AddComponent<EyeCloseController>(); Set(eye, "config", e);
            var eyeOverlay = MakeEyeOverlay(); Set(eye, "eyelids", eyeOverlay.GetComponent<CanvasGroup>());
            var beadGo = new GameObject("Bead Light"); beadGo.transform.SetParent(cameraGo.transform, false); beadGo.transform.localPosition = new Vector3(.2f, -.16f, .15f);
            var light = beadGo.AddComponent<Light>(); light.type = LightType.Spot; light.range = b.lightRange; light.spotAngle = b.lightAngle; light.intensity = b.intensity; light.color = new Color(.62f, .78f, 1f); light.shadows = LightShadows.Soft;
            var bead = player.AddComponent<BeadLight>(); Set(bead, "config", b); Set(bead, "bead", light);
            var nightGo = new GameObject("Camcorder LCD Camera"); nightGo.transform.SetParent(cameraGo.transform, false); nightGo.transform.localPosition = new Vector3(.15f, -.1f, .05f);
            var night = nightGo.AddComponent<Camera>(); night.fieldOfView = c.lcdFov; night.nearClipPlane = .03f; night.farClipPlane = 40; night.enabled = false; night.clearFlags = CameraClearFlags.SolidColor; night.backgroundColor = new Color(.04f,.13f,.055f); nightGo.AddComponent<CamcorderNightVision>();
            night.cullingMask |= 1 << 2;
            var mainCamera = cameraGo.GetComponent<Camera>(); mainCamera.cullingMask &= ~(1 << 2);
            var lcd = MakeLcdCanvas(b);
            var ccamera = player.AddComponent<CamcorderController>(); Set(ccamera, "nightCamera", night); Set(ccamera, "config", c); Set(ccamera, "lcdScreen", lcd.GetComponent<RawImage>());
            var interact = player.AddComponent<InteractionSystem>(); Set(interact, "viewCamera", cam); Set(interact, "config", i);
            return player;
        }

        private static void CreatePlayerPrefab(GameObject player)
        {
            PrefabUtility.SaveAsPrefabAsset(player, "Assets/Prefabs/Player/Player.prefab"); Object.DestroyImmediate(player);
        }

        private static void SetupWorld(string name, Color fog, float density)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat; RenderSettings.ambientLight = new Color(.06f, .07f, .09f);
            RenderSettings.fog = true; RenderSettings.fogMode = FogMode.ExponentialSquared; RenderSettings.fogColor = fog; RenderSettings.fogDensity = density;
            var light = new GameObject("Moonlight", typeof(Light)); light.transform.rotation = Quaternion.Euler(38, -24, 0); var l = light.GetComponent<Light>(); l.type = LightType.Directional; l.color = new Color(.45f, .55f, .75f); l.intensity = .2f; l.shadows = LightShadows.Soft;
            var manager = new GameObject("GameManager"); manager.AddComponent<GameManager>(); SaveSystem.Load();
        }

        private static void AddCheckpoint(string name, Vector3 position, string id)
        {
            var go = new GameObject(name); go.transform.position = position; var col = go.AddComponent<SphereCollider>(); col.isTrigger = true; col.radius = 1f;
            var cp = go.AddComponent<Checkpoint>(); Set(cp, "checkpointId", id);
        }

        private static GameObject Trigger(string name, Vector3 position, Vector3 size)
        {
            var go = new GameObject(name); go.transform.position = position; var c = go.AddComponent<BoxCollider>(); c.isTrigger = true; c.size = size; return go;
        }

        private static GameObject WellProp(Vector3 position, string name)
        {
            var root = Empty(name, position);
            var cylinder = Primitive("Well Ring", PrimitiveType.Cylinder, position, new Vector3(2f, .35f, 2f), "stone", true);
            var inner = Primitive("Dark Water", PrimitiveType.Cylinder, position + Vector3.up * .2f, new Vector3(1.45f, .08f, 1.45f), "water", false);
            Primitive("Well Post", PrimitiveType.Cube, position + new Vector3(0, 1.6f, -.9f), new Vector3(.16f, 3.2f, .16f), "wood", true);
            Primitive("Well Beam", PrimitiveType.Cube, position + new Vector3(0, 3.1f, 0), new Vector3(2.4f, .15f, .15f), "wood", true);
            return root;
        }

        private static GameObject Ground(Vector3 position, Vector3 scale, string name, string material)
        {
            var g = Primitive(name, PrimitiveType.Cube, position, scale, material, true); return g;
        }

        private static GameObject Primitive(string name, PrimitiveType type, Vector3 position, Vector3 scale, string material, bool collider)
        {
            var go = GameObject.CreatePrimitive(type); go.name = name; go.transform.position = position; go.transform.localScale = scale;
            if (!collider && go.TryGetComponent<Collider>(out var c)) Object.DestroyImmediate(c);
            if (go.TryGetComponent<Renderer>(out var r)) r.sharedMaterial = Material(material);
            return go;
        }

        private static Material Material(string key)
        {
            if (Materials.TryGetValue(key, out var cached)) return cached;
            string path = "Assets/Art/Materials/MAT_" + key + ".mat";
            var existing = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (existing != null) { Materials[key] = existing; return existing; }
            Color color = key == "soil" ? new Color(.055f,.052f,.045f) : key == "stone" ? new Color(.11f,.12f,.12f) : key == "wood" ? new Color(.12f,.075f,.045f) : key == "roof" ? new Color(.065f,.052f,.04f) : key == "foliage" ? new Color(.085f,.105f,.075f) : key == "brass" ? new Color(.45f,.27f,.07f) : key == "water" ? new Color(.008f,.02f,.028f) : key == "ghost" ? new Color(.55f,.75f,.8f) : key == "bead" ? new Color(.25f,.7f,1f) : key == "black" ? new Color(.018f,.014f,.02f) : Color.clear;
            var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
            var mat = new Material(shader); mat.color = color; mat.name = "MAT_" + key;
            if (key == "bead") { mat.EnableKeyword("_EMISSION"); mat.SetColor("_EmissionColor", color * 2f); }
            AssetDatabase.CreateAsset(mat, path); Materials[key] = mat; return mat;
        }

        private static GameObject Empty(string name, Vector3 position) { var go = new GameObject(name); go.transform.position = position; return go; }

        private static GameObject AddStaticCamera(string name, Vector3 position)
        {
            var go = new GameObject(name, typeof(Camera), typeof(AudioListener)); go.tag = "MainCamera"; go.transform.position = position;
            var camera = go.GetComponent<Camera>(); camera.clearFlags = CameraClearFlags.SolidColor; camera.backgroundColor = Color.black; camera.nearClipPlane = .03f; camera.farClipPlane = 50f;
            return go;
        }

        private static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer; foreach (Transform child in go.transform) SetLayerRecursively(child.gameObject, layer);
        }

        private static void ApplyCharacterMaterials(GameObject go, bool ghost)
        {
            foreach (var renderer in go.GetComponentsInChildren<Renderer>(true))
            {
                var part = renderer.name.ToLowerInvariant();
                renderer.sharedMaterial = part.Contains("bead") || part.Contains("medallion") ? Material("brass") : ghost ? Material("ghost") : Material("black");
            }
        }

        private static GameObject MakeDialogueCanvas()
        {
            var go = new GameObject("Dialogue UI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(CanvasGroup));
            var canvas = go.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var group = go.GetComponent<CanvasGroup>(); group.alpha = 0; group.blocksRaycasts = false;
            var speaker = MakeText("Speaker", go.transform, new Vector2(.5f,.18f), new Vector2(.8f,.06f), 22, TextAnchor.MiddleCenter);
            var body = MakeText("Body", go.transform, new Vector2(.5f,.1f), new Vector2(.9f,.12f), 18, TextAnchor.MiddleCenter);
            return go;
        }

        private static GameObject MakePromptCanvas()
        {
            var go = new GameObject("Interaction Prompt", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var txt = go.AddComponent<Text>(); txt.font = Font.CreateDynamicFontFromOSFont("Arial", 18); txt.color = Color.white; txt.alignment = TextAnchor.MiddleCenter;
            var rect = txt.rectTransform; rect.anchorMin = new Vector2(.25f,.15f); rect.anchorMax = new Vector2(.75f,.22f); rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;
            return go;
        }

        private static GameObject MakeFadeCanvas()
        {
            var go = new GameObject("Fade Overlay", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(CanvasGroup));
            var canvas = go.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 100;
            var group = go.GetComponent<CanvasGroup>(); group.alpha = 0; group.blocksRaycasts = false;
            var imageGo = new GameObject("Black", typeof(RectTransform), typeof(Image)); imageGo.transform.SetParent(go.transform, false);
            var image = imageGo.GetComponent<Image>(); image.color = Color.black;
            var rect = image.rectTransform; rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;
            return go;
        }

        private static GameObject MakeEyeOverlay()
        {
            var go = new GameObject("Eye Close Overlay", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(CanvasGroup));
            var canvas = go.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 90;
            var group = go.GetComponent<CanvasGroup>(); group.alpha = 0; group.blocksRaycasts = false;
            var imageGo = new GameObject("Closed Eyes", typeof(RectTransform), typeof(Image)); imageGo.transform.SetParent(go.transform, false);
            imageGo.GetComponent<Image>().color = Color.black;
            var rect = imageGo.GetComponent<RectTransform>(); rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;
            return go;
        }

        private static GameObject MakePausePanel()
        {
            var go = new GameObject("Pause Panel", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var text = go.AddComponent<Text>(); text.font = Font.CreateDynamicFontFromOSFont("Arial", 25); text.fontSize = 25; text.color = new Color(.92f,.9f,.82f); text.alignment = TextAnchor.MiddleCenter; text.text = "일시 정지\n\n[ESC] 계속하기\n[Alt+F4] 종료";
            text.rectTransform.anchorMin = new Vector2(.2f,.25f); text.rectTransform.anchorMax = new Vector2(.8f,.75f); text.rectTransform.offsetMin = Vector2.zero; text.rectTransform.offsetMax = Vector2.zero;
            go.SetActive(false); return go;
        }

        private static void MakeControlsHint()
        {
            var go = new GameObject("Controls Hint", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)); go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var text = go.AddComponent<Text>(); text.font = Font.CreateDynamicFontFromOSFont("Arial", 14); text.fontSize = 14; text.color = new Color(.8f,.83f,.83f,.7f); text.alignment = TextAnchor.LowerLeft;
            text.text = "WASD 이동 · 마우스 시선 · Shift 달리기 · Ctrl/C 웅크리기 · E 조사\nF 구슬 조명 · B 캠코더 · V 눈 감기 · Z/X 기울이기 · Esc 일시 정지";
            var r = text.rectTransform; r.anchorMin = new Vector2(.025f,.02f); r.anchorMax = new Vector2(.48f,.12f); r.offsetMin = Vector2.zero; r.offsetMax = Vector2.zero;
        }

        private static GameObject MakeLcdCanvas(BeadConfig config)
        {
            var go = new GameObject("Camcorder LCD", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var imageGo = new GameObject("LCD View", typeof(RectTransform), typeof(RawImage)); imageGo.transform.SetParent(go.transform, false);
            var image = imageGo.GetComponent<RawImage>(); image.color = new Color(.55f, 1f, .62f, 1f);
            var r = image.rectTransform; r.anchorMin = config.viewportPos; r.anchorMax = config.viewportPos; r.sizeDelta = new Vector2(280, 158); r.anchoredPosition = Vector2.zero;
            var rec = MakeText("REC", go.transform, new Vector2(config.viewportPos.x + .105f, config.viewportPos.y - .07f), new Vector2(55, 30), 16, TextAnchor.MiddleCenter); rec.text = "● REC"; rec.color = Color.red;
            return imageGo;
        }

        private static Text MakeText(string name, Transform parent, Vector2 anchor, Vector2 size, int fontSize, TextAnchor alignment)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Text)); go.transform.SetParent(parent, false);
            var text = go.GetComponent<Text>(); text.font = Font.CreateDynamicFontFromOSFont("Arial", fontSize); text.fontSize = fontSize; text.color = new Color(.92f,.9f,.82f); text.alignment = alignment; text.horizontalOverflow = HorizontalWrapMode.Wrap; text.verticalOverflow = VerticalWrapMode.Overflow;
            var r = text.rectTransform; r.anchorMin = anchor; r.anchorMax = anchor; r.sizeDelta = size; r.anchoredPosition = Vector2.zero; return text;
        }

        private static void SaveScene(Scene scene, string path) { EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene, path); }

        private static void Set(Object component, string property, Object value)
        {
            if (component == null) return;
            var so = new SerializedObject(component); var p = so.FindProperty(property);
            if (p == null) { Debug.LogWarning("Serialized field missing: " + component.GetType().Name + "." + property); return; }
            p.objectReferenceValue = value; so.ApplyModifiedPropertiesWithoutUndo();
        }
        private static void Set(Object component, string property, string value)
        {
            if (component == null) return; var so = new SerializedObject(component); var p = so.FindProperty(property); if (p != null) { p.stringValue = value; so.ApplyModifiedPropertiesWithoutUndo(); }
        }
        private static void Set(Object component, string property, float value)
        {
            if (component == null) return; var so = new SerializedObject(component); var p = so.FindProperty(property); if (p != null) { p.floatValue = value; so.ApplyModifiedPropertiesWithoutUndo(); }
        }
    }

}
