using UnityEngine;

public sealed class MoveControlsEvent: siParamEvent<Vector3> {}

public sealed class DataInitCompleteEvent : siEvent {}

public sealed class StorageLoadCompleteEvent : siEvent {}

public sealed class StorageUpdateCompleteEvent : siParamEvent<string> {}

public sealed class LevelStartEvent : siEvent {}

public sealed class LoadProgressEvent : siParamEvent<string> {}

