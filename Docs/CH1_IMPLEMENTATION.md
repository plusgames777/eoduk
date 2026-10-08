# 어둑 — 챕터 1 구현 상태

기획서 기반으로 Unity 프로젝트의 기반과 챕터 1 플레이 흐름을 연결했다. 실행 시작 씬은 `Boot`이며, 잠시 후 `Ch1_Intro`를 연다.

## 플레이 흐름

1. 현대의 우물에서 오프닝 대사와 캠코더 화면 속 구미의 형상을 확인한다. 우물을 조사하면 암전과 함께 1672년 마을로 이동한다.
2. 구미와 상호작용해 구슬 조명을 받는다. 마을의 빈 항아리 소리, 캠코더에서만 보이는 갓 그림자 단서가 배치되어 있다.
3. 마을 길의 낮은 틈은 웅크려 지나간다. 어둑시니가 나타나면 시선 또는 캠코더 화면에 잡힌 동안 커지고, 시선을 피하면 작아진다. 가까이 잡히면 사망 후 마지막 체크포인트로 복귀한다.
4. 귀환 지점에서 구미의 경고가 재생된다. 10초 안에 뒤돌아보지 않으면 강제로 돌아보며 마파람 형상과 마주하고 챕터 완료 화면으로 넘어간다.

## 조작

| 동작 | 키보드·마우스 | 게임패드 |
|---|---|---|
| 이동 / 시선 | WASD / 마우스 | 왼쪽 스틱 / 오른쪽 스틱 |
| 달리기 / 웅크리기 | Shift / Ctrl 또는 C | 왼쪽 스틱 누름 / 오른쪽 스틱 누름 |
| 조사 / 구슬 조명 | E / F | X / Y 탭 |
| 캠코더 / 눈 감기 | B / V | Y 길게 누름 / 왼쪽 트리거 |
| 기울이기 / 일시 정지 | Z·X / Esc | Start로 일시 정지 |

## 주요 에셋

- `Assets/Scenes/Boot.unity`, `Ch1_Intro.unity`, `Ch1_Well.unity`, `Ch1_Complete.unity`
- `Assets/Input/GameInput.inputactions`
- `Assets/Art/Environment/Ch1/SM_Ch1_WellVillage.fbx`
- `Assets/Art/Characters/SK_Gumi.fbx`, `SK_Eoduksini.fbx`
- `Assets/Data/Config/`, `Assets/Data/Gaze/`, `Assets/Resources/Dialogue/`
- `Assets/Scripts/` 아래 입력, 플레이어, 상호작용, 추격 AI, 체크포인트, 대사, 시퀀스 구현

## 확인 상태

- Unity MCP에서 스크립트 컴파일을 확인했고 Chapter 1 씬과 Build Settings에 들어간 씬 목록을 확인했다.
- 화면 캡처로 씬 구성을 살폈다. 실제 플레이 전체 진행과 1080p 성능 수치는 아직 확인하지 않았다. Unity 콘솔에는 이 프로젝트의 C# 컴파일과 별개로 Burst JIT DLL 로드 경고가 남아 있다.
