using UnityEngine;

public class FloorMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool moveHorizontal = true; // ����͹�ǹ͹�����ǵ��

    private Vector3 startPosition;
    private float direction = 1;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // �ӹǳ���˹�����
        float movement = moveSpeed * Time.deltaTime * direction;

        if (moveHorizontal)
        {
            transform.Translate(movement, 0, 0);
            // ����¹��ȷҧ����Ͷ֧�ش����ش
            if (transform.position.x > startPosition.x + moveDistance ||
                transform.position.x < startPosition.x - moveDistance)
            {
                direction *= -1;
            }
        }
        else
        {
            transform.Translate(0, movement, 0);
            // ����¹��ȷҧ����Ͷ֧�ش����ش
            if (transform.position.y > startPosition.y + moveDistance ||
                transform.position.y < startPosition.y - moveDistance)
            {
                direction *= -1;
            }
        }
    }

    // ������ѵ����蹪��Ѻ������͹
    private void OnCollisionEnter(Collision collision)
    {
        // ������ѵ�ط�誹���١�ͧ������͹
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ¡��ԡ������١������͡�ҡ������͹
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
