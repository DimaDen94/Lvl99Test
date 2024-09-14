using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private List<PlacementPoint> _placementPoints;
    [SerializeField] private EnemyData[] _enemyDatas;
    [SerializeField] private StructureData[] _structureDatas;
    [SerializeField] private LvlWaves _lvlWaves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Hero _hero;
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private MainMenuMediator _mainMenuMediator;

    private StructureManager _structureManager;
    private EnemyPoolManager _enemyPoolManager;
    private WaveManager _waveManager;
    private Wallet _wallet;

    private GameStateMachine _gameStateMachine;

    private void Awake() => Initialize();

    private void Initialize()
    {
        IAssetProvider assetProvider = new AssetProvider();
        ActiveEnemyHolder activeEnemyHolder = new ActiveEnemyHolder();
        StructureHolder structureHolder = new StructureHolder();
        EnemyFactory enemyFactory = new EnemyFactory(_enemyDatas, _hero, _spline.Spline, assetProvider);
        StructureFactory structureFactory = new StructureFactory(assetProvider, activeEnemyHolder);
        UIFactory uiFactory = new UIFactory(assetProvider);
        SessionTimer sessionTimer = new  SessionTimer(this);
        _wallet = new Wallet();

        _enemyPoolManager = new EnemyPoolManager(enemyFactory, CountEnemiesByType(_lvlWaves), activeEnemyHolder);
        _structureManager = new StructureManager(structureFactory, uiFactory, _placementPoints, _wallet, _structureDatas, structureHolder);
        _waveManager = new WaveManager(_enemyPoolManager, _spawnPoint, _lvlWaves, activeEnemyHolder);


        _gameStateMachine = new GameStateMachine(_hero, _waveManager, _wallet, _structureManager, _enemyPoolManager, uiFactory, activeEnemyHolder, structureHolder, _mainMenuMediator, sessionTimer);
    }

    private void Start() => _gameStateMachine.Enter<GameBootstappState>();

    private void Update() => _gameStateMachine.Update();

    private Dictionary<EnemyType, int> CountEnemiesByType(LvlWaves lvlWaves)
    {
        var waveCounter = new EnemyWaveCounter();
        return waveCounter.CountEnemiesByType(lvlWaves);
    }
}