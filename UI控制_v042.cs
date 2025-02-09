using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;

public class UI控制_v042 : MonoBehaviour
{
    [Header("窗口控制")]
    public GameObject WinSetting;
    [Header("UI")]
    public GameObject 置顶UI;
    public GameObject 锁定穿透UI;
    public GameObject 样式1_obj;
    public GameObject 样式2_obj;
    //public GameObject 解锁穿透UI;

    [Header("右键菜单")]
    public GameObject 右键菜单;
    public Button 风格切换Button;
    public Button 是否置顶Button;
    public Button 锁定穿透Button;
    public Button 帮助Button;
    public Button 关闭菜单Button;
    public string 帮助文档 = "Readme.txt";

    [Header("键盘监听器")]
    public GameObject 键盘监听器;

    [Header("背景图")]
    public SpriteRenderer 图片背景;
    float x;
    public string 背景图路径 = "资源/背景图";
    public string DataXml = "资源/Data.xml";
    float 透明度;

    // Start is called before the first frame update
    void Start()
    {
        置顶UI.SetActive(WinSetting.GetComponent<WinSetting_v042>().是否置顶);
        锁定穿透UI.SetActive(WinSetting.GetComponent<WinSetting_v042>().是否穿透窗口);
        //解锁穿透UI.SetActive(!WinSetting.GetComponent<WinSetting_4>().是否穿透窗口);
        if (键盘监听器.GetComponent<KeyButtonListener_v042>().样式切换 == true)
        {
            样式1_obj.SetActive(true);
            样式2_obj.SetActive(false);
        }
        else
        {
            样式1_obj.SetActive(false);
            样式2_obj.SetActive(true);
        }

        if (图片背景 == null)
        {
            图片背景 = new GameObject().AddComponent<SpriteRenderer>();
            图片背景.transform.SetAsFirstSibling();
            图片背景.sortingOrder = -4;
        }
        if (!Directory.Exists("资源"))
        {
            Directory.CreateDirectory("资源");
        }

        读取1();
        加载图片(背景图路径);

        风格切换Button.onClick.AddListener(风格切换);

        是否置顶Button.onClick.AddListener(WinSetting.GetComponent<WinSetting_v042>().是否置顶判断);
        是否置顶Button.onClick.AddListener(关闭右键菜单);//WinSetting_4脚本中没有关于右键菜单关闭的方法，所以需要另外注册监听器来关闭右键菜单

        锁定穿透Button.onClick.AddListener(WinSetting.GetComponent<WinSetting_v042>().是否穿透判断);
        锁定穿透Button.onClick.AddListener(关闭右键菜单);

        帮助Button.onClick.AddListener(打开帮助文档);

        关闭菜单Button.onClick.AddListener(关闭右键菜单);
        右键菜单.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        背景控制();
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            保存1();
        }

        if (WinSetting.GetComponent<WinSetting_v042>().是否置顶 == true)
        {
            置顶UI.SetActive(true);
        }
        else
        {
            置顶UI.SetActive(false);
        }
        if (WinSetting.GetComponent<WinSetting_v042>().是否穿透窗口 == true)
        {
            锁定穿透UI.SetActive(true);
            //解锁穿透UI.SetActive(false);
        }
        else
        {
            锁定穿透UI.SetActive(false);
            //解锁穿透UI.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            右键菜单.SetActive(true);
            右键菜单.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        }


    }
    void 加载图片(string relativePath) //参考GPT
    {
        string appDirectory = Directory.GetCurrentDirectory();
        string[] possiblePaths = new string[]
        {
            Path.Combine(appDirectory, relativePath + ".jpg"),
            Path.Combine(appDirectory, relativePath + ".png")
        };
        string fullPath = null;

        // 检查文件是否存在
        foreach (string path in possiblePaths)
        {
            if (File.Exists(path))
            {
                fullPath = path;
                break;
            }
        }
        if (fullPath != null)
        {
            byte[] fileData = File.ReadAllBytes(fullPath);  // 读取文件数据
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);  // 将数据加载为纹理

            // 创建 Sprite 并赋值给 UI Image
            图片背景.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            图片背景.sortingOrder = -4;
        }
        else
        {
            图片背景.color = Color.white;
        }
    }

    public void 背景控制()
    {
        透明度 = Mathf.Clamp(图片背景.color.a, 0, 1);
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Equals))
        {
            透明度 += Time.deltaTime / 2;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Minus))
        {
            透明度 -= Time.deltaTime / 2;
        }
        if (透明度 == 0)
        {
            图片背景.gameObject.SetActive(false);
        }
        else
        {
            图片背景.gameObject.SetActive(true);
        }
        图片背景.color = new Color(图片背景.color.r, 图片背景.color.g, 图片背景.color.b, 透明度);

        x = Mathf.Clamp(图片背景.transform.localScale.x, 0, 100);
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftBracket))//缩放
        {
            x -= x * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightBracket))
        {
            x += x * Time.deltaTime;
        }
        图片背景.transform.localScale = new Vector3(x, x,1);


        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.UpArrow))//上移
        {
            图片背景.transform.position += new Vector3(0, Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.DownArrow))//下移
        {
            图片背景.transform.position -= new Vector3(0, Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftArrow))//左移
        {
            图片背景.transform.position -= new Vector3(Time.deltaTime * 5, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightArrow))//右移
        {
            图片背景.transform.position += new Vector3(Time.deltaTime * 5, 0);
        }
    }

    void 保存1()//https://wenku.csdn.net/answer/c0bb4d9fff4a4caf92fa781cfd1b8104
    {
        XmlDocument xml = new XmlDocument();
        XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", "");

        XmlElement Root = xml.CreateElement("Data");//根节点
        XmlElement BackgroundImageInfo = xml.CreateElement("BackgroundImageInfo");
        //XmlElement KeyBoardListener_style = xml.CreateElement("KeyBoardListener_style");

        BackgroundImageInfo.SetAttribute("localScale", 图片背景.transform.localScale.ToString());
        BackgroundImageInfo.SetAttribute("Position", 图片背景.transform.position.ToString());
        BackgroundImageInfo.SetAttribute("Color", 图片背景.color.ToString());

        //KeyBoardListener_style.SetAttribute("style", 键盘监听器.GetComponent<KeyButtonListener_3>().样式切换.ToString());

        Root.AppendChild(BackgroundImageInfo);
        //Root.AppendChild(KeyBoardListener_style);

        xml.AppendChild(Root);

        xml.Save(DataXml);//相对路径保存
    }

    void 读取1()
    {
        if (File.Exists(DataXml))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(DataXml);
            XmlNodeList nodeList = xml.SelectSingleNode("Data").ChildNodes;//查找根节点
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == "BackgroundImageInfo")//查找子节点
                {
                    图片背景.transform.localScale = Vector2Parse.StrToVector2(xe.GetAttribute("localScale"));

                    图片背景.transform.position = Vector2Parse.StrToVector2(xe.GetAttribute("Position"));

                    图片背景.color = ColorParse.StrToColor(xe.GetAttribute("Color"));
                }
                //if (xe.Name == "KeyBoardListener_style")
                //{

                //}
            }
        }
        else
        {
            Debug.Log("没有Data.xml文件");
        }
    }

    public void 风格切换()
    {
        右键菜单.SetActive(false);
        键盘监听器.GetComponent<KeyButtonListener_v042>().样式切换 = !键盘监听器.GetComponent<KeyButtonListener_v042>().样式切换;
        if (键盘监听器.GetComponent<KeyButtonListener_v042>().样式切换 == true)
        {
            样式1_obj.SetActive(true);
            样式2_obj.SetActive(false);
        }
        else
        {
            样式1_obj.SetActive(false);
            样式2_obj.SetActive(true);
        }
    }

    public void 打开帮助文档()
    {
        Application.OpenURL(帮助文档);
    }

    public void 关闭右键菜单()
    {
        右键菜单.SetActive(false);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            右键菜单.SetActive(false);
        }
    }
}

