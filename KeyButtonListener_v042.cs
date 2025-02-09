using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButtonListener_v042 : MonoBehaviour
{
    [HideInInspector]
    public bool ��ʽ�л� = false;

    [Header("��ʽ1")]
    public ��ʽ1class ��ʽ1;
    [Header("��ʽ2")]
    public ��ʽ2class ��ʽ2;

    [HideInInspector]
    public bool ����л� = false;


    void Start()
    {

    }
    void Update()
    {
        if (��ʽ�л� == true)//��UI����������
        {
            ��ʽ1.KeyandMouseClass_1();
        }
        else
        {
            ��ʽ2.KeyandMouseClass_2();
        }
    }

    [System.Serializable]
    public class ��ʽ1class
    {
        public List<KeyClass_1> ����;
        public MouseClass_1 ���;

        public void KeyandMouseClass_1()
        {
            for (int a = 0; a < ����.Count; a++)
            {
                ����[a].KeyContral();
            }
            ���.MouseContral_1();
        }
        [System.Serializable]
        public class KeyClass_1
        {
            public SpriteRenderer ��������ͼ;
            public TextMeshPro ����;
            public GlobalKeyCode Ҫ�󶨵İ���;
            public Color ��������������ɫ = Color.blue;
            public Color ��������ԭʼ��ɫ = Color.white;

            public void KeyContral()
            {
                if (GlobalKeyboard.IsKeyPressed(Ҫ�󶨵İ���))
                {
                    ��������ͼ.color = ��������������ɫ;
                    if (����)
                    {
                        ����.color = Color.white;
                    }
                }
                else
                {
                    ��������ͼ.color = ��������ԭʼ��ɫ;
                    if (����)
                    {
                        ����.color = Color.black;
                    }
                }
            }
        }
        [System.Serializable]
        public class MouseClass_1
        {
            public SpriteRenderer ���;
            public SpriteRenderer �м�;
            public SpriteRenderer �Ҽ�;
            public SpriteRenderer ���1;
            public SpriteRenderer ���2;
            public Color ��������������ɫ = Color.blue;
            public Color ��������ԭʼ��ɫ = Color.white;
            public void MouseContral_1()
            {
                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.������))
                {
                    ���.color = ��������������ɫ;
                }
                else
                {
                    ���.color = ��������ԭʼ��ɫ;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.����м�))
                {
                    �м�.color = ��������������ɫ;
                }
                else
                {
                    �м�.color = ��������ԭʼ��ɫ;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.����Ҽ�))
                {
                    �Ҽ�.color = ��������������ɫ;
                }
                else
                {
                    �Ҽ�.color = ��������ԭʼ��ɫ;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X1��갴ť))
                {
                    ���1.color = ��������������ɫ;
                }
                else
                {
                    ���1.color = ��������ԭʼ��ɫ;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X2��갴ť))
                {
                    ���2.color = ��������������ɫ;
                }
                else
                {
                    ���2.color = ��������ԭʼ��ɫ;
                }
            }
        }
    }

    [System.Serializable]
    public class ��ʽ2class
    {
        public List<KeyClass_2> ����;
        public MouseClass_2 ���;

        public void KeyandMouseClass_2()
        {
            for (int a = 0; a < ����.Count; a++)
            {
                ����[a].KeyContral_2();
            }
            ���.MouseContral_2();
        }

        [System.Serializable]
        public class KeyClass_2
        {
            public GlobalKeyCode Ҫ�󶨵İ���;
            public GameObject ��������ͼ;
            public GameObject ��������;
            public GameObject ����ԭʼ;
            public void KeyContral_2()
            {
                if (GlobalKeyboard.IsKeyPressed(Ҫ�󶨵İ���))
                {
                    ��������ͼ.SetActive(true);

                    ��������.SetActive(true);
                    ����ԭʼ.SetActive(false);
                }
                else
                {
                    ��������ͼ.SetActive(false);

                    ��������.SetActive(false);
                    ����ԭʼ.SetActive(true);
                }
            }
        }

        [System.Serializable]
        public class MouseClass_2
        {
            [Header("���")]
            public GameObject ���_ԭʼ;
            public GameObject ���_����;
            [Header("�м�")]
            public GameObject �м�_ԭʼ;
            public GameObject �м�_����;
            [Header("�Ҽ�")]
            public GameObject �Ҽ�_ԭʼ;
            public GameObject �Ҽ�_����;
            [Header("���1")]
            public GameObject ���1_ԭʼ;
            public GameObject ���1_����;
            [Header("���2")]
            public GameObject ���2_ԭʼ;
            public GameObject ���2_����;
            public void MouseContral_2()
            {
                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.������))
                {
                    ���_����.SetActive(true);
                    ���_ԭʼ.SetActive(false);
                }
                else
                {
                    ���_����.SetActive(false);
                    ���_ԭʼ.SetActive(true);
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.����м�))
                {
                    �м�_����.SetActive(true);
                    �м�_ԭʼ.SetActive(false);
                }
                else
                {
                    �м�_����.SetActive(false);
                    �м�_ԭʼ.SetActive(true);
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.����Ҽ�))
                {
                    �Ҽ�_����.SetActive(true);
                    �Ҽ�_ԭʼ.SetActive(false);
                }
                else
                {
                    �Ҽ�_����.SetActive(false);
                    �Ҽ�_ԭʼ.SetActive(true);
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X1��갴ť))
                {
                    ���1_����.SetActive(true);
                    ���1_ԭʼ.SetActive(false);
                }
                else
                {
                    ���1_����.SetActive(false);
                    ���1_ԭʼ.SetActive(true);
                }
                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X2��갴ť))
                {
                    ���2_����.SetActive(true);
                    ���2_ԭʼ.SetActive(false);
                }
                else
                {
                    ���2_����.SetActive(false);
                    ���2_ԭʼ.SetActive(true);
                }
            }
        }
    }
}
