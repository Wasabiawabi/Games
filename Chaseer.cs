using UnityEngine;

public class Chaseer : MonoBehaviour
{
    // Unity�����Player�I�u�W�F�N�g��������ϐ���p��
    public GameObject player;
    public float speed = 2.0f; // �ړ����x
    Vector3 movespeed = Vector3.zero;
    public float limit = 10.0f;
    public float  bounceFactor = 0.5f;//���˕Ԃ�W��
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�����݂���ꍇ
        if (player != null)
        {
            //�v���C���[�Ɍ�������movespeed������.movespeed��speed�̒~��
            //x,y���W���Ƃ�movespeed��speed�����Z
            movespeed.x += (player.transform.position.x - transform.position.x) * speed * Time.deltaTime;
            movespeed.y += (player.transform.position.y - transform.position.y) * speed * Time.deltaTime;
            transform.position += movespeed * Time.deltaTime;
            //movespeed�̏���𒴂��������l�ɕς���
            if (movespeed.x > 0 && movespeed.x > limit)
            {
                movespeed.x = limit;
            }
            else if (movespeed.x < 0 && movespeed.x < -limit)
            {
                movespeed.x = -limit;
            }
            //y���W��movespeed�����l�ɐ�����������
            if (movespeed.y > 0 && movespeed.y > limit)
            {
                movespeed.y = limit;
            }
            else if (movespeed.y < 0 && movespeed.y < -limit)
            {
                movespeed.y = -limit;
            }
        }
    }
    //�������������璵�˕Ԃ�
    void OnCollisionEnter2D(Collision2D collision)
    {
        //�����ɓ��������璵�˕Ԃ� �^�O��Moves�̃I�u�W�F�N�g�ɓ��������ꍇ
        if (collision.gameObject.tag == "Moves")
        {
            // ���������I�u�W�F�N�g�̖@���x�N�g�����擾
            Vector2 normal = collision.contacts[0].normal;
            // �@���x�N�g�����g���Ē��˕Ԃ�������v�Z
            Vector2 bounceDirection = Vector2.Reflect(movespeed, normal);
            // ���˕Ԃ�����Ƀo�E���X�W�����|����movespeed���X�V
            movespeed = bounceDirection * bounceFactor;
            //���������I�u�W�F�N�g�̈ʒu���擾���āA�����Ɍ������Ĉړ�����
            Vector3 hitPosition = collision.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, hitPosition, speed * Time.deltaTime);
        }
    }
}
