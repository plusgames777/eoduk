어둑 — Tasks (작업 단계)
한국 전통설화 1인칭 공포게임 「어둑」 · 최종본 (2026.10)
어떤 순서로 만드는가. "어둑" Spec을 AI 에이전트가 한 번의 프롬프트로 처리할 수 있는 작업(T-번호)으로 나눈 로드맵이다. 작업마다 선행 작업, 대상 파일, 근거(Spec 절), 검수 기준(Acceptance의 AC-번호)을 지정한다.
0. 작업 규칙
한 번에 하나의 작업만 수행한다. 선행 작업이 "완료"가 아니면 시작하지 않는다.
대상 파일 칸에 없는 파일은 만들거나 고치지 않는다. 꼭 필요하면 먼저 묻는다.
작업 전 Spec의 근거 절을 다시 읽는다.
작업 중 Spec에 없는 새 미정 항목이 생기면 추측하지 말고 사용자에게 먼저 확인하고, 답에 맞춰 Spec을 먼저 고친다(현재 미정 항목은 없음).
레벨 그레이박스는 Spec 3.10의 챕터별 지형(경사, 고도, 바닥 재질, 길 폭, 앉아서 지나는 통로)과 3.11의 바람 구역을 따른다. 각 챕터 통합 검수에서 AC-112·AC-113의 해당 항목을 함께 점검한다.
레벨은 Spec 1.9의 챕터 목표 시간, 무이벤트 최대 90초, 체크포인트 간격 8분을 따른다. 긴 구간(챕터 2 구간 C, 챕터 4 구간 E)에는 이벤트 비트 목록에서 골라 60~90초마다 비트를 두고, 각 챕터 통합 검수에서 AC-114로 측정한다. 기본 도형 레벨로 동작을 먼저 완성하고, 에셋 교체는 마일스톤 마지막 작업에서 한다. 마지막 작업(통합 검수)이 통과해야 다음 마일스톤을 시작한다.
작업이 끝나면 아래 형식으로 보고하고 상태 칸을 갱신한다.
보고 형식
[T-번호] 작업명
상태: 완료 / 부분 완료 / 막힘
생성·수정 파일: (경로 목록)
변경 요약: (3줄 이내)
AC 점검: AC-번호 각 항목 통과 / 미통과 / 확인 불가(사유)
질문: (있으면)
상태 표기: ☐ 대기 · ◐ 진행 중 · ☑ 완료 · ✕ 막힘
마일스톤 개요
마일스톤 | 내용 | 작업 수 | 목표 플레이 시간
M0 | 프로젝트 기반 (설정, 입력, 이벤트, 저장, 대사) | 6 | 
M1 | 코어 시스템 (걷기·뛰기·앉기, 여우구슬 광원, 캠코더, 눈 감기, 상호작용, 시선, 어둑시니, 체크포인트, 메뉴, 후처리, 지형·경사 물리와 바람, 플레이타임 기록기) | 13 | 
M2 | 챕터 1 버티컬 슬라이스 (평지, 0m) | 5 | 15~25분
M3 | 챕터 2 시스템 (스태미나, 숨 참기, 은신, 배터리, 인벤토리, 일지, 정화, 구슬, 추격, 보스 약화 아이템) | 11 | 
M4 | 챕터 2 콘텐츠와 주지 보스 (오르막 0 → 500m) | 5 | 35~50분
M5 | 챕터 3 (퇴마 도구, 피격, 석상, 결계·봉인, 무당 보스, 정상부, 강풍) | 8 | 40~55분
M6 | 챕터 4 (내리막 500 → 0m, 이중 시선, 전환점, 떼어내기 보스) | 6 | 30~40분
M7 | 챕터 5·엔딩 (평지 아레나, 두억시니 3페이즈, 회상, 재회, 엔딩) | 6 | 25~35분
M8 | 마감 (완주, 성능, 한국어·영어 자막) | 3 | 전체 2시간 30분~3시간 30분
모든 미정 항목은 2026.10에 일괄 확정되었다(Spec 6.3). 마일스톤 시작 전 확인할 항목은 없다.
M0. 프로젝트 기반
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-001 | Unity LTS + URP 프로젝트 생성, 폴더 구조, Git·LFS, Docs 복사 | 없음 | 프로젝트 골격, .gitattributes, .gitignore, Docs/*.md, Docs/VERSION.md | Spec 1.4, 1.5 / AC-001
☐ T-002 | Input Actions 에셋(Player, UI 맵, 키보드·마우스와 Xbox 패드 두 스킴)과 입력 래퍼 | T-001 | Assets/Input/GameInput.inputactions, Scripts/Core/InputRouter.cs | Spec 5.1 / AC-002
☐ T-003 | 설정 ScriptableObject 클래스와 기본값 에셋 전부(CFG_, GAZE_, CFG_Boss_*) | T-001 | Scripts/Core/Config/*.cs, Data/Config/*.asset, Data/Gaze/*.asset, Tests/EditMode/ConfigDefaultsTests.cs | Spec 2.1~2.3 / AC-003
☐ T-004 | GameEvents, GameManager, AudioManager, SceneLoader, Boot 씬과 타이틀 | T-003 | Scripts/Core/GameEvents.cs, GameManager.cs, AudioManager.cs, SceneLoader.cs, Scenes/Boot.unity | Spec 0.5, 2.6 / AC-004
☐ T-005 | SaveSystem (save.json, settings.json) | T-004 | Scripts/Core/SaveSystem.cs, SaveData.cs, Tests/EditMode/SaveTests.cs | Spec 2.4 / AC-005
☐ T-006 | 대사 데이터 구조, 자막 UI, DLG_Ch1 빈 테이블 | T-004 | Scripts/Core/DialogueLine.cs, Scripts/UI/DialogueUI.cs, Data/Dialogue/DLG_Ch1.asset | Spec 2.4, 3.9 / AC-006
M1. 코어 시스템
모든 작업은 Scenes/Test_Core.unity에서 검증한다. 이 씬은 T-101에서 만들고 이후 작업이 테스트 구역을 추가한다.
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-101 | PlayerController: 걷기·뛰기·앉기(앉은 상태에서 방향키로 앉은 채 이동, 전환 0.3초, 머리 위 공간 검사), 고개 내밀기, 헤드밥, 발소리와 NoiseMade, 테스트 씬 | T-002, T-003 | Scripts/Player/PlayerController.cs, Prefabs/Player/PF_Player.prefab, Scenes/Test_Core.unity | Spec 3.1 / AC-101
☐ T-102 | 여우구슬 광원: 화면 왼쪽 위 뷰모델(임시 구체), Spot·Point Light, 켜기·끄기, 주지용 경직 판정, Flicker API | T-101 | Scripts/Player/BeadLight.cs, FlickerPattern.cs | Spec 3.2 / AC-102
☐ T-103 | 캠코더: LCD RenderTexture, 나이트비전 셰이더, REC 오버레이, LcdOnlyVisual | T-101 | Scripts/Player/CamcorderController.cs, LcdOnlyVisual.cs, Art/FX/NightVision.shader | Spec 3.3 / AC-103
☐ T-104 | 눈 감기 (최대 4초, 쿨 3초) | T-101 | Scripts/Player/EyeCloseController.cs | Spec 3.4 / AC-104
☐ T-105 | 상호작용 Tap·Hold, 프롬프트와 진행 원 UI | T-101 | Scripts/Systems/InteractionSystem.cs, IInteractable.cs, Scripts/UI/InteractPromptUI.cs | Spec 3.5 / AC-105
☐ T-106 | GazeSystem, IGazeTarget, 테스트용 타깃, 자동 테스트 | T-103, T-104 | Scripts/Gaze/GazeSystem.cs, IGazeTarget.cs, GazeTestTarget.cs, Tests/PlayMode/GazeTests.cs | Spec 3.6 / AC-106
☐ T-107 | EoduksiniAI: 크기·속도·잡기·은신 대응 뼈대·NavMesh (임시 캡슐 외형) | T-106 | Scripts/Enemies/EoduksiniAI.cs, Prefabs/Enemies/PF_Eoduksini.prefab, Tests/PlayMode/EoduksiniTests.cs | Spec 3.7, 2.2 / AC-107
☐ T-108 | 어둑시니 외형 셰이더(_Spread, _VeilReveal), 안개 파티클, 저주파·심장박동·젖은 숨소리 | T-107 | Art/FX/Eoduksini.shader, Art/FX/VFX_EoduksiniMist, Scripts/Enemies/ProximityAudio.cs | Spec 3.7, 1.8 / AC-108
☐ T-109 | 체크포인트, 사망 연출, 상태 복원 | T-005, T-107 | Scripts/Systems/Checkpoint.cs, DeathHandler.cs | Spec 3.8 / AC-109
☐ T-110 | 일시정지와 설정 (리바인딩, 볼륨, 헤드밥, 감도, 자막 언어) | T-005, T-006 | Scripts/UI/PauseMenu.cs, SettingsMenu.cs, Prefabs/UI/PF_PauseMenu.prefab | Spec 3.9 / AC-110
☐ T-111 | 후처리 Volume 프로필, 높이 안개 셰이더 | T-001 | Data/Config/PP_Default.asset, Art/FX/HeightFog.shader | Spec 1.4 / AC-111
☐ T-112 | 지형·이동 물리와 바람: CFG_Movement, 경사 보정, 미끄러짐, SurfaceType 7종, 소음 반경, 적의 경사 보정(어둑시니 제외), WindSystem(구역 볼륨, 돌풍, 맞바람·뒷바람, 소음 감쇠, 돌풍 때 안개 걷힘, 전역 셰이더 값, 바람 사운드 3단). Test_Core에 경사 램프(0·5·10·15·20·25·30·40·45°), 돌계단, 재질 패드, 바람 구역 추가 | T-101, T-107, T-111 | Scripts/Player/MovementPhysics.cs, Scripts/Systems/SurfaceType.cs, NoiseEmitter.cs, WindSystem.cs, WindZoneVolume.cs, Data/Config/CFG_Movement.asset, CFG_Wind.asset, Tests/PlayMode/MovementTests.cs, WindTests.cs | Spec 3.10, 3.11 / AC-112, AC-113
☐ T-113 | 플레이타임 기록기(개발 빌드 전용): 챕터·구간별 경과 시간, 사망 횟수, 이벤트 사이 최대 간격, 체크포인트 사이 진행 시간을 CSV로 저장. 레벨의 이벤트 비트에 붙이는 PacingBeat 컴포넌트 | T-109 | Scripts/Core/PlaytimeRecorder.cs, Scripts/Systems/PacingBeat.cs, Tests/PlayMode/PlaytimeRecorderTests.cs | Spec 1.9 / AC-114
M2. 챕터 1 (버티컬 슬라이스)
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-201 | Ch1_Intro 그레이박스: 현대 우물 촬영 오프닝(달빛·나이트비전), LCD 수면의 구미, 대사, 손가락에 끌려 들어가는 타임리프 컷 | M1 전체 | Scenes/Ch1_Intro.unity, Scripts/Sequences/TimeLeapSequence.cs, DLG_Ch1 대사 입력 | Spec 5.3 챕터 1-① / AC-201
☐ T-202 | Ch1_Well 그레이박스(평지, 바람 0.2): 구미가 여우구슬을 건네는 연출, 조작 안내, 앉아서 지나는 담 틈, 체크포인트 4곳 | T-201 | Scenes/Ch1_Well.unity, Scripts/Sequences/GumiGuide.cs, BeadHandoverSequence.cs | Spec 5.3 챕터 1-②③, 3.10 / AC-202
☐ T-203 | 빈 독 긁는 소리, LCD 복선 실루엣, 첫 조우 스폰, "돌아보지 마" 마지막 컷(10초 뒤 숨소리와 함께 강제로 돌아봄) | T-202 | Scripts/Sequences/ForeshadowTrigger.cs, FinalTurnSequence.cs | Spec 5.3 챕터 1-③④⑤ / AC-203
☐ T-204 | 챕터 1 에셋 제작·교체 (Blender MCP): 현대·1672년 우물, 마을, 뷰모델(손·캠코더·여우구슬), 어둑시니(너울), 구미 반투명 | T-203 | Art/Environment/Ch1/*, Art/ViewModel/*, Art/Characters/Eoduksini/*, Art/Characters/Gumi/* | Spec 1.8, 6.1 / AC-204
☐ T-205 | 챕터 1 통합 검수 (키보드·패드 완주, 지형·바람·페이싱, 성능) | T-204 | 없음 (보고서만) | AC-205, AC-112·113·114 챕터 1 항목
M3. 챕터 2 시스템
Test_Core에 테스트 구역을 추가해 검증한다.
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-301 | ChapterConfig 적용기: 챕터별 입력·시스템 플래그, 시선 프로필·바람 구역 세트 교체 | M2 전체 | Scripts/Core/ChapterConfigApplier.cs, Data/Config/CFG_Chapter_1~5.asset | Spec 2.1, 5.2 / AC-301
☐ T-302 | 스태미나 (평지 6초 / 2초 / 4초, 앉아 있을 때 회복 1.25배, 오르막 걷기 숨참) | T-301 | Scripts/Player/StaminaSystem.cs, PlayerController.cs(뛰기 연동) | Spec 4.3, 3.10 / AC-302
☐ T-311 | 숨 참기: 평소 숨소리(정지 1.5m, 헐떡임 6m)와 NoiseMade, 우클릭·RT 길게로 참기(최대 5초, 뛰기 불가, 발소리 × 0.5), 헐떡임 6m·터짐 10m, 스태미나 소모, 화면·심장박동 연출, 추격 중 허용 | T-302 | Scripts/Player/BreathController.cs, Data/Config/CFG_Breath.asset, Tests/PlayMode/BreathTests.cs | Spec 3.12 / AC-311
☐ T-303 | 은신 지점(앉은 상태), 어둑시니의 추적 상실·배회 | T-302 | Scripts/Systems/HidingSpot.cs, Scripts/Enemies/EoduksiniAI.cs(수정) | Spec 3.7, 4.3 / AC-303
☐ T-304 | 캠코더 배터리 소모와 교체 (R) | T-301 | Scripts/Player/CamcorderBattery.cs, CamcorderController.cs(수정) | Spec 4.4 / AC-304
☐ T-305 | ItemDef, 인벤토리, 빠른 슬롯, 지도 탭(구간 이름과 고도) | T-301 | Scripts/Systems/ItemDef.cs, InventoryLite.cs, Scripts/UI/InventoryUI.cs, MapUI.cs, Data/Items/*.asset | Spec 2.5, 4.4 / AC-305
☐ T-306 | 촬영과 일지 (Story, Relic, Photo) | T-305 | Scripts/Systems/PhotoCapture.cs, Photographable.cs, JournalSystem.cs, Scripts/UI/JournalUI.cs, Data/Items/Journal/*.asset | Spec 4.4 / AC-306
☐ T-307 | 정화 시스템, 정화력, 사연 컷, 혼령 대상 | T-305 | Scripts/Systems/PurificationSystem.cs, IPurifiable.cs, SpiritTarget.cs, Scripts/Sequences/StoryCutPlayer.cs | Spec 4.1 / AC-307
☐ T-308 | 여우구슬 확장: 위급 깜빡임(20m·10m·추격), 체력 색, 길 안내, 반응 단계, 보스 퇴마·흡수 공용 컴포넌트, 구슬 단계와 저장, 최종 뷰모델 | T-307 | Scripts/Systems/BeadController.cs, BeadWaypoint.cs, BeadExorcism.cs, Art/ViewModel/Bead/* | Spec 4.2 / AC-308
☐ T-309 | 추격 연출기: 세트피스 트리거, ChaseChanged, 입력 잠금 | T-303 | Scripts/Enemies/ChaseDirector.cs, ChaseTrigger.cs | Spec 3.1, 5.2 / AC-309
☐ T-310 | 보스 약화 아이템 공용 구조: 사용형(던지기·놓기·걸기·말뚝에 사용)·소지형·설치형, 7종 ItemDef, IWeakenable, 일지 유래 문구, 테스트용 보스 더미 | T-305, T-308 | Scripts/Systems/BossWeakenItem.cs, IWeakenable.cs, PlaceableItem.cs, ThrowableItem.cs, Data/Items/Weaken/*.asset | Spec 4.11 / AC-310
M4. 챕터 2 콘텐츠 (오르막 0 → 500m)
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-401 | Ch2_MountainPath 그레이박스 구간 A~C(경사·고도·바닥·바람 구역 Spec 3.10·3.11), 구슬 변신, 숨 참기 튜토리얼(구간 C), 혼령·시신 정화 대상, 발목 잡는 손, letter_yun·yucheok·염주·공양미 주머니, 은신 지점, 이정표와 원경, 구간 C의 이벤트 비트(60~90초 간격, PacingBeat), 체크포인트 1~4, 대사 | M3 전체 | Scenes/Ch2_MountainPath.unity, Data/Dialogue/DLG_Ch2.asset, Scripts/Sequences/BeadTransformSequence.cs | Spec 5.3 챕터 2-①~⑤ / AC-401
☐ T-402 | 구간 B 오르막 추격 세트피스 | T-401 | Ch2_MountainPath.unity(세트피스 배치) | Spec 5.3 챕터 2-④ / AC-402
☐ T-403 | Ch2_TempleGate 그레이박스: 돌계단 약 170단, 정상(500m)의 사찰 외관 원경(단층 전각, 불빛 없음), 공양 터, 일주문, 바람 0.7, 체크포인트 5 | T-401 | Scenes/Ch2_TempleGate.unity | Spec 5.3 챕터 2-⑥⑦ / AC-403
☐ T-404 | 주지 보스: 소리로만 찾는 눈먼 순찰, 발우 긁는 위치음, 속삭임 때 청각 2배(숨소리 감지, 숨 참기로 회피), 구휼 기록 3장, 구슬 빛 경직, 퇴마·흡수, 일주문 개방, 보스전 중 어둑시니 대기, 약화 아이템(공양미 주머니·염주) 효과 | T-403 | Scripts/Enemies/BossHeadMonk.cs, Data/Config/CFG_Boss_HeadMonk.asset, Prefabs/Enemies/PF_HeadMonk.prefab, Tests/PlayMode/HeadMonkTests.cs | Spec 4.9, 4.11 / AC-404, AC-310
☐ T-405 | 챕터 2 에셋 제작·교체(산길, 시신 비탈, 능선, 돌계단, 일주문, 공양 터, 주지, 혼령, 도깨비불, 이정표, 구름바다)와 통합 검수 | T-404 | Art/Environment/Ch2/*, Art/Characters/HeadMonk/*, Art/FX/* | Spec 1.8, 6.1 / AC-405, AC-112·113·114 챕터 2 항목
M5. 챕터 3 (정상부 485~515m)
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-501 | IStunnable(CanBeStunnedNow 포함), 퇴마 도구(소금 부채꼴·바람 보정, 부적 단일), 수량, 소금 보충 제상 | M4 전체 | Scripts/Systems/IStunnable.cs, Scripts/Player/ExorcismToolController.cs, Scripts/Systems/SaltRefillAltar.cs | Spec 4.5 / AC-501
☐ T-502 | PlayerHealth, 피격 연출(구슬 빛 주황 → 빨간불 맥동, 가장자리 어두워짐), 3회 사망 | T-501 | Scripts/Player/PlayerHealth.cs, Scripts/UI/HitVignette.cs | Spec 4.6, 4.2 / AC-502
☐ T-503 | 나한상: 보면 멈춤, 안 보면 접근, DarkZone 규칙, 경직·정화 | T-502 | Scripts/Enemies/StatueEnemyAI.cs, Scripts/Systems/DarkZone.cs, Prefabs/Enemies/PF_Arhat.prefab | Spec 3.6, 4.7 / AC-503
☐ T-504 | 순찰형 적 3종 (사천왕 Guard, 망자 Swarm, 악귀 Wander), 소리 감지 | T-502 | Scripts/Enemies/PatrolEnemyAI.cs, PF_HeavenlyKing, PF_Damned, PF_Wraith | Spec 4.7 / AC-504
☐ T-505 | 결계와 봉인석 4기, 결계 강도, 결계 밖 실루엣 단계, 봉인마다 바람 +0.05, 봉인 완성 시 오방기가 빛나며 인벤토리로 복귀 | T-501 | Scripts/Systems/BarrierController.cs, SealManager.cs, SealStone.cs | Spec 4.8 / AC-505
☐ T-506 | Ch3_Temple 그레이박스(테라스 오르내림, 마루, 앉아서 지나는 통로, 바람 0.8): 천왕문 → 굿 흔적 → 천왕문 → 나한전·명부전 → 산신각 동선, 오방기·명두 거울·팥 주머니 배치, 나한전 구슬 깜빡임 연출, 구미 진실 장면, 체크포인트, 대사 | T-503, T-504, T-505 | Scenes/Ch3_Temple.unity, Data/Dialogue/DLG_Ch3.asset, Scripts/Sequences/GumiTruthSequence.cs | Spec 5.3 챕터 3, 3.10, 3.11 / AC-506
☐ T-507 | 무당 보스: 1페이즈(방울 예고, 오방기 4색), 2페이즈(팔 휩쓸기, 향 연기 공격과 혼미(숨 참기로 버팀), 무방비 틈 경직 → 정화 3회), 황색 오방기 마무리, 퇴마·흡수, 오방기 5색 회수, 약화 아이템(팥 주머니·명두 거울) 효과, 무당 대사 | T-506 | Scripts/Enemies/BossShaman.cs, Data/Config/CFG_Boss_Shaman.asset, Prefabs/Enemies/PF_Shaman.prefab, Tests/PlayMode/ShamanTests.cs | Spec 4.9, 4.11 / AC-507, AC-310
☐ T-508 | 사찰 모듈러 키트, 대웅전 내부, 굿 소품, 석상·망자·무당 에셋 제작·교체와 통합 검수 | T-507 | Art/Environment/Temple/*, Art/Props/Gut/*, Art/Characters/Shaman/*, Art/Characters/Statues/* | Spec 1.8, 6.1 / AC-508, AC-112·113·114 챕터 3 항목
M6. 챕터 4 (내리막 500 → 0m)
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-601 | GAZE_Ch4 적용, 잡히는 거리의 배율 보간(1.2 → 1.8m) | M5 전체 | Data/Gaze/GAZE_Ch4.asset, Scripts/Enemies/EoduksiniAI.cs(수정) | Spec 2.2 / AC-601
☐ T-602 | Ch4_Descent 그레이박스 구간 A~E(경사·고도·바닥·바람이 내려갈수록 약해짐), 일주문 밖 기습(컷신 없음, 문 닫힘), 절벽 난간길 바위 틈 2곳과 보이지 않는 벽, 말뚝 뽑힘·안개 아래 얼굴 연출, 구간 E의 이벤트 비트(60~90초 간격, PacingBeat), 체크포인트 1~5, 대사 | T-601 | Scenes/Ch4_Descent.unity, Data/Dialogue/DLG_Ch4.asset | Spec 5.3 챕터 4, 3.10, 3.11 / AC-602
☐ T-603 | 무너진 암자 이중 시선 구간(나한상 앞, 어둑시니 뒤), 도구·약화 아이템 보급(복숭아나무 가지·쑥 다발) | T-602 | Ch4_Descent.unity(구간 C) | Spec 4.7, 5.3 챕터 4-③ / AC-603
☐ T-604 | 전환점: 넘어짐·palm_wounded, 15초 조작 제한, _VeilReveal, 기억 조각 플래시, 구미 대사, 사슬 등장, turning_point_done | T-602 | Scripts/Sequences/MemoryWaverEvent.cs | Spec 4.10 / AC-604
☐ T-605 | 떼어내기 보스: 짚 제웅 말뚝 3개 정화, 어둑시니 경직·축소·퇴각, 윤태호의 신음과 구슬 금빛, 구슬 흡수, 약화 아이템(복숭아나무 가지·쑥 다발) 효과 | T-604 | Scripts/Enemies/ChainAnchorBoss.cs, ChainAnchor.cs, Data/Config/CFG_Boss_ChainAnchor.asset | Spec 4.9, 4.11 / AC-605, AC-310
☐ T-606 | 챕터 4 에셋 제작·교체(절벽 난간길, 무너진 암자, 파괴된 공양 터, 사슬·말뚝)와 통합 검수 | T-605 | Art/Environment/Ch4/*, Art/Props/Chains/* | Spec 1.8 / AC-606, AC-112·113·114 챕터 4 항목
M7. 챕터 5·엔딩 (평지 0m)
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-701 | Ch5_WellBoss 아레나 그레이박스(돌담 4, 제상 2, 장대 5, 봉인석, 소용돌이 바람), 두억시니 1페이즈(휩쓸기·역병 안개·움켜쥐기, 안개 직후 2초 열린 구멍에만 소금 경직, 우물 들여다보기 금지와 유혹 목소리, 어둑시니 추격 지속), 약화 아이템(엄나무 가지·쑥 다발·팥 주머니·명두 거울) 효과 | M6 전체 | Scenes/Ch5_WellBoss.unity, Scripts/Enemies/BossDueoksini.cs, WellPeekTrigger.cs, Data/Config/CFG_Boss_Dueoksini.asset, Tests/PlayMode/DueoksiniTests.cs | Spec 4.9, 4.11 / AC-701, AC-310
☐ T-702 | 2페이즈: 사슬 5가닥, 사방 장대에 오방기 꽂기(Hold 2초), 사슬 절단 효과 | T-701 | Scripts/Systems/ObanggiPole.cs, BossDueoksini.cs(수정) | Spec 4.9 / AC-702
☐ T-703 | 3페이즈: 중앙 황색 오방기, 시선 반전, 최종 봉인 Hold 5초(손바닥 피와 구슬, 구슬 흡수, 구슬 단계 4), 들여다보기 규칙 해제와 우물 속 비명 | T-702 | Scripts/Gaze/GazeReversal.cs, Scripts/Systems/FinalSealController.cs | Spec 4.9, 4.10 / AC-703
☐ T-704 | 회상 시퀀스 5조각 (2.5D 레이어, 자막, 클리어 후 스킵 가능) | T-703 | Scripts/Sequences/FlashbackSequencer.cs, Art/Flashback/* | Spec 4.10 / AC-704
☐ T-705 | 재회: 구미 본래 모습, 사람 모습 윤태호, mapae 전달, 구미의 고백 | T-704 | Scripts/Sequences/ReunionSequence.cs, Data/Dialogue/DLG_Ch5.asset | Spec 4.10 / AC-705
☐ T-706 | Ch5_Ending: 현대 복귀, 사당, 캠코더 재생(빈 화면), 크레딧, 크레딧 후 장면 + 챕터 5 통합 검수 | T-705 | Scenes/Ch5_Ending.unity, Scripts/Sequences/EndingSequence.cs, Scripts/UI/Credits.cs | Spec 4.10 / AC-706, AC-112·113·114 챕터 5 항목
M8. 마감
ID | 작업 | 선행 | 대상 파일 | 근거 / 검수
☐ T-801 | 처음부터 엔딩까지 완주 검수 (키보드·마우스 1회, 패드 1회), 문제 목록 작성 | M7 전체 | Docs/PLAYTEST_REPORT.md | AC-801
☐ T-802 | 성능 최적화: LOD 3단계, GPU 인스턴싱, 오클루전 컬링, 안개·바람 셰이더 비용 점검 | T-801 | 각 씬, Docs/PERF_REPORT.md | Spec 1.2 / AC-802
☐ T-803 | 영어 자막 번역 입력(모든 대사 줄의 textEn, 영문 소설판 문체 기준)과 자막 언어 전환 마무리 | T-801 | Data/Dialogue/*, SettingsMenu.cs | Spec 1.3 / AC-803