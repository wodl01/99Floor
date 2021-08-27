using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObjectScript : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] PlayerState playerState;

    [SerializeField] bool isHardOb;
    [SerializeField] bool angleToPlayer;
    [SerializeField] GameObject[] objectPiece;
    [SerializeField] int spawnPiecesAmount;
    [SerializeField] bool spawnPieceWhenHit;
    [SerializeField] bool spawnPieceWhenDestroy;
    [SerializeField] bool spawnAtOb;

    [Header("State")]
    float hp;
    public bool isPassingBy;
    [SerializeField] int MaxHp;
    [SerializeField] int pieceForce;
    bool isDestroyed = false;
    [SerializeField] GameObject hitedObTrans;

    private void OnEnable()
    {
        playerState = PlayerState.playerState;
        hp = MaxHp;
    }

    public void BrokenObHit(GameObject hitedOb,int bulletCode,float dmg)
    {
        if (isDestroyed) return;
        if (bulletCode == 0 && isHardOb) return;
        hp -= dmg;
        hitedObTrans = hitedOb;
        if (spawnPieceWhenHit) SpawnPieces();
        if (hp <= 0)
        {
            if (spawnPieceWhenDestroy) SpawnPieces();
            isDestroyed = true;
            gameObject.SetActive(false);
        }
    }

    void SpawnPieces()
    {
        SoundManager.Play("Destroy");
        for (int i = 0; i < spawnPiecesAmount; i++)
        {
            int randomPieceNum = Random.Range(0, objectPiece.Length);

            if (angleToPlayer)
            {
                GameObject Piece = Instantiate(objectPiece[randomPieceNum], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                float mainAngle = mainAngle = Mathf.Atan2(hitedObTrans.transform.position.y - gameObject.transform.position.y, hitedObTrans.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg + Random.Range(-40, 40);
                Vector3 force = Quaternion.AngleAxis(mainAngle, Vector3.forward) * Vector3.right;
                Piece.GetComponent<Rigidbody2D>().AddForce(-force * pieceForce);
            }
            else
            {
                GameObject Piece = Instantiate(objectPiece[randomPieceNum], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                float mainAngle = hitedObTrans.transform.eulerAngles.z + Random.Range(-40, 40);
                Vector3 force = Quaternion.AngleAxis(mainAngle, Vector3.forward) * Vector3.right;
                Piece.GetComponent<Rigidbody2D>().AddForce(force * pieceForce);
            }
        }
    }
}
