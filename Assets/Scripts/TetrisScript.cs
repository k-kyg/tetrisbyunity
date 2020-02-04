using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TetrisScript : MonoBehaviour
{
	public GameObject tetriMino;
	public GameObject field;
	public GameObject[] minos;
	private string[] minonames;
	private float timer;
	private string framename = "";
	private int[,] map;
	private Vector3 before = new Vector3(0, 20, -1);
	public bool isColliding = false;
	public List<string> framenames = new List<string>();
	// Start is called before the first frame update
	void Start()
	{
		int minonum = Random.Range(0, 7);
		map = new int[12, 21];
		minonames = new string[7];
		for (int i = 0; i < 21; ++i)
		{
			for (int j = 0; j < 12; ++j)
			{
				if (i == 20)
				{
					map[j, i] = 1;
					break;
				}
				if (j == 0 || j == 11) map[j, i] = 1;
			}
		}
		tetriMino = Instantiate(minos[minonum], new Vector3(0, 20, -1), Quaternion.identity);
		{
			int i = 0;
			foreach (var mino in minos)
			{
				minonames[i] = mino.transform.name;
				++i;
			}
		}
		/*
		foreach(var minoname in minonames)
		{
			Debug.Log(minoname);
		}
		*/
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) && framename != "leftframe") tetriMino.transform.position += Vector3.left;
		if (Input.GetKeyDown(KeyCode.RightArrow) && framename != "rightframe") tetriMino.transform.position += Vector3.right;
		timer += Time.deltaTime;
		if (isColliding)
		{
			isColliding = false;
			tetriMino.transform.position = before;
			return;
		}
		if (framename == "bottomframe" || framename.Contains("Cube"))
		{
			timer = 0;
			before += Vector3.up;
			tetriMino.transform.position = before;
			framename = "";
			this.DicideMino();
			return;
		}
		else if (timer >= 0.7f)
		{
			timer = 0;
			Debug.Log(framename);
			tetriMino.transform.position += Vector3.down;
		}
		before = tetriMino.transform.position;
	}
	private void OnTriggerEnter(Collider other)
	{
		// isColliding = true;
		/*
			---Memo---
			ここで、beforeの変数に入れておいた座標に戻せるようにできるといいな。でも、TetriMinoにrigidbody入れてないから当たり判定が出ずに困っています...
			でもTetriMinoにrigidbodyを入れると逆にframeのほうの名前が出なくなってしまって枠からはみ出てしまう...
			もしかして、ぜんぶどっちにしろ一つ戻す処理にしとけば全部大丈夫なのかな？
			あと、一番下についたときに暴れ回ってるとなんとすり抜けてしまうことがあるというバグがある...
			っていうことを考えながら2020/02/03はもう夜遅くなってしまったので寝ました。
			---end---
		*/
	}
	private void OnTriggerExit(Collider other)
	{
		// isColliding = false;
	}
	public void SetFrameName(string collisioningframe)
	{
		this.framename = collisioningframe;
	}
	public string GetFrameName() => this.framename;
	public Vector3 SetBefore(Vector3 before) => this.before = before;
	private int DicideMino()
	{
		before = new Vector3(0, 20, -1);
		int minonum = Random.Range(0, 7);
		tetriMino = Instantiate(minos[minonum], new Vector3(0, 20, -1), Quaternion.identity);
		return minonum;
	}
}
