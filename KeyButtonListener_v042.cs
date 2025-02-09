using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButtonListener_v042 : MonoBehaviour
{
    [HideInInspector]
    public bool 样式切换 = false;

    [Header("样式1")]
    public 样式1class 样式1;
    [Header("样式2")]
    public 样式2class 样式2;

    [HideInInspector]
    public bool 侧键切换 = false;


    void Start()
    {

    }
    void Update()
    {
        if (样式切换 == true)//由UI控制器决定
        {
            样式1.KeyandMouseClass_1();
        }
        else
        {
            样式2.KeyandMouseClass_2();
        }
    }

    [System.Serializable]
    public class 样式1class
    {
        public List<KeyClass_1> 按键;
        public MouseClass_1 鼠标;

        public void KeyandMouseClass_1()
        {
            for (int a = 0; a < 按键.Count; a++)
            {
                按键[a].KeyContral();
            }
            鼠标.MouseContral_1();
        }
        [System.Serializable]
        public class KeyClass_1
        {
            public SpriteRenderer 按键背景图;
            public TextMeshPro 文字;
            public GlobalKeyCode 要绑定的按键;
            public Color 按键背景亮显颜色 = Color.blue;
            public Color 按键背景原始颜色 = Color.white;

            public void KeyContral()
            {
                if (GlobalKeyboard.IsKeyPressed(要绑定的按键))
                {
                    按键背景图.color = 按键背景亮显颜色;
                    if (文字)
                    {
                        文字.color = Color.white;
                    }
                }
                else
                {
                    按键背景图.color = 按键背景原始颜色;
                    if (文字)
                    {
                        文字.color = Color.black;
                    }
                }
            }
        }
        [System.Serializable]
        public class MouseClass_1
        {
            public SpriteRenderer 左键;
            public SpriteRenderer 中键;
            public SpriteRenderer 右键;
            public SpriteRenderer 侧键1;
            public SpriteRenderer 侧键2;
            public Color 按键背景亮显颜色 = Color.blue;
            public Color 按键背景原始颜色 = Color.white;
            public void MouseContral_1()
            {
                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.鼠标左键))
                {
                    左键.color = 按键背景亮显颜色;
                }
                else
                {
                    左键.color = 按键背景原始颜色;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.鼠标中键))
                {
                    中键.color = 按键背景亮显颜色;
                }
                else
                {
                    中键.color = 按键背景原始颜色;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.鼠标右键))
                {
                    右键.color = 按键背景亮显颜色;
                }
                else
                {
                    右键.color = 按键背景原始颜色;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X1鼠标按钮))
                {
                    侧键1.color = 按键背景亮显颜色;
                }
                else
                {
                    侧键1.color = 按键背景原始颜色;
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X2鼠标按钮))
                {
                    侧键2.color = 按键背景亮显颜色;
                }
                else
                {
                    侧键2.color = 按键背景原始颜色;
                }
            }
        }
    }

    [System.Serializable]
    public class 样式2class
    {
        public List<KeyClass_2> 按键;
        public MouseClass_2 鼠标;

        public void KeyandMouseClass_2()
        {
            for (int a = 0; a < 按键.Count; a++)
            {
                按键[a].KeyContral_2();
            }
            鼠标.MouseContral_2();
        }

        [System.Serializable]
        public class KeyClass_2
        {
            public GlobalKeyCode 要绑定的按键;
            public GameObject 按键背景图;
            public GameObject 文字亮显;
            public GameObject 文字原始;
            public void KeyContral_2()
            {
                if (GlobalKeyboard.IsKeyPressed(要绑定的按键))
                {
                    按键背景图.SetActive(true);

                    文字亮显.SetActive(true);
                    文字原始.SetActive(false);
                }
                else
                {
                    按键背景图.SetActive(false);

                    文字亮显.SetActive(false);
                    文字原始.SetActive(true);
                }
            }
        }

        [System.Serializable]
        public class MouseClass_2
        {
            [Header("左键")]
            public GameObject 左键_原始;
            public GameObject 左键_亮显;
            [Header("中键")]
            public GameObject 中键_原始;
            public GameObject 中键_亮显;
            [Header("右键")]
            public GameObject 右键_原始;
            public GameObject 右键_亮显;
            [Header("侧键1")]
            public GameObject 侧键1_原始;
            public GameObject 侧键1_亮显;
            [Header("侧键2")]
            public GameObject 侧键2_原始;
            public GameObject 侧键2_亮显;
            public void MouseContral_2()
            {
                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.鼠标左键))
                {
                    左键_亮显.SetActive(true);
                    左键_原始.SetActive(false);
                }
                else
                {
                    左键_亮显.SetActive(false);
                    左键_原始.SetActive(true);
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.鼠标中键))
                {
                    中键_亮显.SetActive(true);
                    中键_原始.SetActive(false);
                }
                else
                {
                    中键_亮显.SetActive(false);
                    中键_原始.SetActive(true);
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.鼠标右键))
                {
                    右键_亮显.SetActive(true);
                    右键_原始.SetActive(false);
                }
                else
                {
                    右键_亮显.SetActive(false);
                    右键_原始.SetActive(true);
                }

                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X1鼠标按钮))
                {
                    侧键1_亮显.SetActive(true);
                    侧键1_原始.SetActive(false);
                }
                else
                {
                    侧键1_亮显.SetActive(false);
                    侧键1_原始.SetActive(true);
                }
                if (GlobalKeyboard.IsKeyPressed(GlobalKeyCode.X2鼠标按钮))
                {
                    侧键2_亮显.SetActive(true);
                    侧键2_原始.SetActive(false);
                }
                else
                {
                    侧键2_亮显.SetActive(false);
                    侧键2_原始.SetActive(true);
                }
            }
        }
    }
}
