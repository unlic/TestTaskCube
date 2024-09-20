using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private List<Hero> enamys;
    [SerializeField] private List<Material> backGrounds;
    [SerializeField] private MeshRenderer backGround;


    [SerializeField] private Cube cubePrefab;
    [SerializeField] private RectTransform spawnPlayerHandsPoint;
    [SerializeField] private RectTransform spawnEnamyHandsPoint;
    [SerializeField] private Button startRoll;

    [SerializeField] private Player player;
    [SerializeField] private Player enamy;

    [SerializeField] private PopupCubeInfo popupCubeInfo;

    public Action<int> WinAction;
    public Action<int> LoseAction;


    private int currentLevel = 1;
    private float stepSpawn = 1.5f;
    private float startStawn = -3f;

    private Action RollAction;
    private Action ReturnToStartPositionAction;

    private Cube enamyCube;
    private List<Cube> playerCubes = new();

    private bool isPlayerRolling;
    private bool isPlayerTurn;

    private void Start()
    {
        player.DieAction += Lose;
        enamy.DieAction += Win;

        startRoll.onClick.AddListener(StartDiceRoll);
    }

    public void StartLevel(int level)
    {
        RollAction = null;
        ReturnToStartPositionAction = null;
        isPlayerRolling = false;
        isPlayerTurn = false;
        currentLevel = level;
        int enamyNumber = currentLevel;

        if (currentLevel > enamys.Count)
        {
            enamyNumber = UnityEngine.Random.Range(0, enamys.Count+1);
        }


        foreach (var handData in hero.TakeHandsList())
        {
            Cube cube = Instantiate(cubePrefab, new Vector3(startStawn, 5, spawnPlayerHandsPoint.position.z), spawnPlayerHandsPoint.rotation);
            cube.SetSpritesInCube(handData);
            cube.OnClickCubeAction += ClickOnPlayerCube;
            playerCubes.Add(cube);
            RollAction += cube.RollDice;
            ReturnToStartPositionAction += cube.ReturnToStartingPosition;

            startStawn += stepSpawn;
        }

        foreach (var handData in enamys[enamyNumber - 1].TakeHandsList())
        {
            enamyCube = Instantiate(cubePrefab, new Vector3(0, 5, spawnEnamyHandsPoint.position.z), spawnEnamyHandsPoint.rotation);
            enamyCube.SetSpritesInCube(handData);
            enamyCube.OnClickCubeAction += ShowPopup;
        }

        if(currentLevel == 1)
            player.SetData(hero);


        enamy.SetData(enamys[enamyNumber - 1]);

        backGround.material = backGrounds[enamyNumber - 1];

        StartCoroutine(EnemyStepRoll());
    }

    private IEnumerator EnemyStepRoll()
    {
        
        yield return new WaitForSeconds(1);
        enamyCube.RollDice();
        yield return new WaitForSeconds(6);
        enamyCube.ReturnToStartingPosition();
    }

    private void StartDiceRoll()
    {
        if(isPlayerRolling)
            return;
        RollAction?.Invoke();
        StartCoroutine(ReturnToStartPosition());
    }

    private IEnumerator ReturnToStartPosition()
    {
        yield return new WaitForSeconds(6);
        ReturnToStartPositionAction?.Invoke();
        isPlayerRolling = true;
    }

    private void ClickOnPlayerCube(HandData data)
    {
        if (!isPlayerRolling)
        {
            ShowPopup(data);
            return;
        }
        StartCoroutine(PlayerTurn(data));
        isPlayerRolling = false;
    }

    private IEnumerator PlayerTurn(HandData data)
    {
        if (!isPlayerRolling)
        {
            ShowPopup(data);
            yield break;
        }
        isPlayerTurn = true;
        Turn(data.TakeDataByCurrentSide());

        yield return new WaitForSeconds(2);
        isPlayerTurn = false;
        Turn(enamyCube.TakeCubeHandData().TakeDataByCurrentSide());
        yield return new WaitForSeconds(0.3f);

        player.StepEnd();
        enamy.StepEnd();


        yield return new WaitForSeconds(0.3f);
        StartCoroutine(EnemyStepRoll());

    }

    private void Turn(Side data)
    {   
        switch (data.Type)
        {
            case SideType.Attack:
                Attack(data.Power);
                break;
            case SideType.AttackAndShield:
                AttackAndShield(data.Power, data.Buf);
                break;
            case SideType.VenomAttack:
                VenomAttack(data.Power, data.Debuf);
                break;
            case SideType.Shield:
                Shield(data.Power);
                break;
            case SideType.Heals:
                Heals(data.Power);
                break;
        }
    }

    private void Attack(int damage)
    {
        Player playerToAttack = isPlayerTurn? enamy:player;

        playerToAttack.TakeDamage(damage);
    }

    private void AttackAndShield(int damage, int buf)
    {
        Player playerToAttack = isPlayerTurn ? enamy : player;
        Player playerToBuf = !isPlayerTurn ? enamy : player;

        playerToAttack.TakeDamage(damage);
        playerToBuf.AddBuf(buf);
    }

    private void VenomAttack(int damage, int debuf)
    {
        Player playerToAttack = isPlayerTurn ? enamy : player;

        playerToAttack.TakeDamage(damage);
        playerToAttack.AddDebuf(debuf);
    }


    private void Shield(int buf)
    {
        Player playerToBuf = !isPlayerTurn ? enamy : player;

        playerToBuf.AddBuf(buf);
    }

    private void Heals(int healsValue)
    {
        Player playerToBuf = !isPlayerTurn ? enamy : player;

        playerToBuf.Heals(healsValue);
    }

    private void ShowPopup(HandData data)
    {
        popupCubeInfo.SetData(data);
    }

    private void Win()
    {
        WinAction?.Invoke(currentLevel);
        ResetLevel();
    }

    private void Lose()
    {
        LoseAction?.Invoke(currentLevel);
        ResetLevel();
    }

    private void ResetLevel()
    {
        StopAllCoroutines();
        player.ResetBufAndDebufData();
        enamy.ResetBufAndDebufData();
        Destroy(enamyCube.gameObject);
        foreach (var cube in playerCubes)
        {
            Destroy(cube.gameObject);
        }
        playerCubes.Clear();
        startStawn = -3f;
    }
}
