# Watch

Интерактивное приложение с часами на Unity. Время синхронизируется с сервером, а пользователь может менять его через аналоговые и цифровые часы.

An interactive clock app built with Unity. Time is synced from a server, and the user can adjust it via analog and digital watches.

---

## Русский

### Описание

**Watch** — 2D-приложение на Unity с двумя типами часов, работающими от единого источника времени:

- **Аналоговые часы** — стрелки часов, минут и секунд; время можно менять перетаскиванием ближайшей стрелки мышью.
- **Цифровые часы** — отображение времени в формате `HH:mm:ss`; время можно ввести вручную в текстовое поле.

При запуске приложение запрашивает текущее UTC-время через API и далее обновляет его локально каждый кадр. Изменение времени на одних часах сразу отражается на остальных.

### Стек

- Unity **2022.3.62f3**
- Universal Render Pipeline (URP)
- Addressables — асинхронная загрузка основной сцены
- TextMeshPro — UI цифровых часов
- Целевая платформа: **WebGL** (сборка в `Builds/watches/`)

### Архитектура

```
Preloader → AppManager → запрос времени → загрузка MainScene (Addressables)
                ↓
           TimeManager ←→ SceneManager ←→ Watch[] (HandClock, DigitalWatch)
```

| Модуль | Назначение |
|--------|------------|
| `AppManager` | Точка входа: инициализация, запрос времени, загрузка сцены |
| `TimeManager` | Хранение UTC-времени, тик каждый кадр, обработка команд |
| `SceneManager` | Связь `TimeManager` с массивом часов на сцене |
| `HandClock` | Поворот стрелок по текущему времени |
| `HandDragController` | Перетаскивание стрелок мышью |
| `DigitalWatch` | Отображение и ручной ввод времени |

### Запуск

1. Откройте проект в Unity 2022.3.
2. Убедитесь, что в Build Settings включена сцена `Assets/Scenes/Preloader.unity`.
3. Запустите Play Mode или соберите WebGL-билд.

Готовый WebGL-билд можно открыть через локальный HTTP-сервер из папки `Builds/watches/`.

---

## English

### Description

**Watch** is a Unity 2D app with two clock types driven by a single time source:

- **Analog clock** — hour, minute, and second hands; drag the nearest hand with the mouse to set the time.
- **Digital clock** — time shown as `HH:mm:ss`; edit the value directly in a text field.

On startup, the app fetches the current UTC time from an API and then advances it locally every frame. Changing time on one watch updates all others immediately.

### Tech stack

- Unity **2022.3.62f3**
- Universal Render Pipeline (URP)
- Addressables — async main scene loading
- TextMeshPro — digital clock UI
- Target platform: **WebGL** (build output in `Builds/watches/`)

### Architecture

```
Preloader → AppManager → fetch time → load MainScene (Addressables)
                ↓
           TimeManager ←→ SceneManager ←→ Watch[] (HandClock, DigitalWatch)
```

| Module | Role |
|--------|------|
| `AppManager` | Entry point: init, time fetch, scene load |
| `TimeManager` | UTC time storage, per-frame tick, command handling |
| `SceneManager` | Bridges `TimeManager` and on-scene watches |
| `HandClock` | Rotates hands to match current time |
| `HandDragController` | Mouse drag interaction for hands |
| `DigitalWatch` | Display and manual time input |

### Getting started

1. Open the project in Unity 2022.3.
2. Ensure `Assets/Scenes/Preloader.unity` is enabled in Build Settings.
3. Enter Play Mode or build for WebGL.

Serve the `Builds/watches/` folder over HTTP to run the WebGL build locally.

---

## Структура проекта / Project structure

```
Assets/
├── Scenes/
│   ├── Preloader.unity      # Стартовая сцена / Startup scene
│   └── MainScene.unity      # Основная сцена с часами / Main clock scene
├── Scripts/
│   ├── Core/                # AppManager, SceneLoader, SceneManager
│   ├── Times/               # TimeManager, команды / commands
│   └── Watches/             # HandClock, DigitalWatch, HandDragController
└── AddressableAssetsData/   # Настройки Addressables / Addressables config
Builds/watches/              # WebGL-сборка / WebGL build
```
