using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Mono.Data.SqliteClient;
using STORY_GAMEDATA;
using STORY_ENUM;

//#define DB_PATH "samplegame.png"

public class Sqlprocess : MonoBehaviour
{
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;

	private string connection;

    #region SingleTon
    public static Sqlprocess SingleInstance { get; set; }

    public static Sqlprocess Instance
    {
        get
        {
            if (SingleInstance == null)
            {
                SingleInstance = GameObject.FindObjectOfType(typeof(Sqlprocess)) as Sqlprocess;

                if (SingleInstance == null)
                {
                    GameObject Container = new GameObject();

                    SingleInstance = Container.AddComponent(typeof(Sqlprocess)) as Sqlprocess;
                    SingleInstance.Init();
                    Container.name = SingleInstance.ToString();
                }
            }

            return SingleInstance;
        }
    }
    #endregion


    public void Init()
    {
    }

	void Awake()
	{
		GameObject.DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () 
	{
		OpenDB ( "samplegame.db" );

		int bCreate = PlayerPrefs.GetInt ("UserDBCreate");
	
		if (bCreate <= 0) 
		{
			CreateUserTable();
		}
    }

	bool CreateUserTable()
	{

		string query = "CREATE TABLE userData( Init INTEGER, gold INTEGER, cash INTEGER, level INTEGER, exp INTEGER )";
		BasicQuery ( query );

		query = "CREATE TABLE userCharData( Row INTEGER, nIndex INTEGER, uID INTEGER, strName TEXT, Type INTEGER, Attribut INTEGER, Lv INTEGER, PromotionLv INTEGER, " +
			"RevolutionLv INTEGER, Pos INTEGER, PosType INTEGER, strImage TEXT, AI INTEGER, AtDist REAL, AtDelay REAL, MoveSpeed REAL, BattleDeck INTEGER, Exp INTEGER, PRIMARY KEY(Row) )";
		BasicQuery (query);

		query = "CREATE TABLE userCharStatData( Row INTEGER, uID INTEGER, Str INTEGER, Dex INTEGER, Inte INTEGER, StrValue REAL, DexValue REAL, HPValue REAL, " +
			"MPValue REAL, PowerValue REAL, PhysicsValue REAL, MagicValue REAL, PhysicsAccuracyValue REAL, MagicAccuracyValue REAL, PhysicsCriValue REAL, " +
				"MagicCriValue REAL, PhysicsDefecsValue REAL, MagicDefecsValue REAL, PhysicsMissValue REAL, MagicMissValue REAL, PRIMARY KEY(Row) )";
		BasicQuery (query);

		initUserChar();

		PlayerPrefs.SetInt ("UserDBCreate", 1919);

		return true;
	}


    bool initUserChar()
    {
		//if ( LoadIntgerData( "SELECT Init FROM userData" ) <= 0 ) 
		{
			BasicQuery ("INSERT INTO userData( Init, gold, cash, level, exp ) VALUES(1, 1000, 10000, 1, 0)");


			OBJECT_DEFAULT_DATA dat = GameDataManagerChar.Instance.GetCharInfoData (100001);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100001);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100002);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100008);
			InsertCharData (dat);
			
			dat = GameDataManagerChar.Instance.GetCharInfoData (100009);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100010);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100011);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100011);
			InsertCharData (dat);

			dat = GameDataManagerChar.Instance.GetCharInfoData (100011);
			InsertCharData (dat);
		}

        return true;
    }

    public void OpenDB(string strPath)
    {
		connection = "URI=file:" + Application.dataPath + "/06.Data/" + strPath;

		if (Application.platform == RuntimePlatform.Android)
		{
			string filepath = Application.persistentDataPath + "/" + strPath;
			
			if (!File.Exists (filepath)) 
			{
				WWW loadDB = new WWW ("jar:file://" + Application.dataPath + "!/assets/" + strPath);  // this is the path to your StreamingAssets in android
				
				while (!loadDB.isDone) 
				{
				}  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

				File.WriteAllBytes (filepath, loadDB.bytes);

				PlayerPrefs.SetInt ("UserDBCreate", 0);
			}
			
			connection = "URI=file:" + filepath;
		} 
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			connection = "Data Source=" + Application.persistentDataPath + "/" + strPath;
		} 
		else 
		{	
			if (!File.Exists (Application.dataPath + "/06.Data/" + strPath))
			{
				PlayerPrefs.SetInt ("UserDBCreate", 0);
			} 
		}
        
		//Debug.Log(connection);
		dbcon = new SqliteConnection(connection);
        dbcon.Open();
	}
	
	
	/*
	public void OpenDB(string p)
	{
		Debug.Log("Call to OpenDB:" + p);
		// check if file exists in Application.persistentDataPath
		//string filepath = Application.persistentDataPath + "/" + p;
        string filepath = Application.dataPath + "/06.Data/" + p;
        if(!File.Exists(filepath))
		{
			Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
			                 Application.dataPath + "!/assets/" + p);
			// if it doesn't ->
			// open StreamingAssets directory and load the db -> 
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "/assets/" + p);
			while(!loadDB.isDone) {}
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDB.bytes);
		}
		
		//open db connection
		connection = "URI=file:" + filepath;
		Debug.Log("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}
	*/
	public void CloseDB(){
		reader.Close(); // clean everything up
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	}
	
	public IDataReader BasicQuery(string query)
	{ // run a basic Sqlite query

        dbcmd = dbcon.CreateCommand(); // create empty command
        dbcmd.CommandText = query; // fill the command
		reader = dbcmd.ExecuteReader();
         // execute command which returns a reader
        return reader; // return the reader
	}

    public bool CreateTable(string query)
    { // Create a table, name, column array, column type array
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
            return false;
        }
        return true;
    }


	public int LoadIntgerData( string strQuery )
	{
		BasicQuery (strQuery);
		reader.Read ();

		return reader.GetInt32 (0);
	}

	public bool UpdateIntgerData( string strQuery )
	{
		BasicQuery (strQuery);
		return true;
	}

	public USER_DATA LoadUserData()
	{
		BasicQuery ("SELECT* FROM userData");

		while (reader.Read()) 
		{
			USER_DATA objDat = new USER_DATA();
			
			objDat.nMoney 			= reader.GetInt32( 1 );
			objDat.nCash			= reader.GetInt32( 2 );
			objDat.nLevel			= reader.GetInt32( 3 );
			objDat.nExp 			= reader.GetInt32( 4 );
			return objDat;
		}
		
		return null;
	}

	public bool InsertCharData( OBJECT_DEFAULT_DATA dat )
    {
		//CREATE TABLE userCharData( Row INTEGER, nIndex INTEGER, uID INTEGER, strName TEXT, Type INTEGER, Attribut INTEGER, Lv INTEGER, PromotionLv INTEGER, RevolutionLv INTEGER, Pos INTEGER, strImage TEXT, AI INTEGER, PRIMARY KEY(Row) )
		//CREATE TABLE userCharData( Row INTEGER, nIndex INTEGER, uID INTEGER, strName TEXT, Type INTEGER, Attribut INTEGER, Lv INTEGER, PromotionLv INTEGER, RevolutionLv INTEGER, Pos INTEGER, strImage TEXT, AI INTEGER, PRIMARY KEY(Row) )
		//Debug.Log (dat.nIndex.ToString ());

		if (dat == null) {
			Debug.Log( "InsertCharData" );
			return false;
		}

		int nDamageType = (int)dat.DamageType;


		string query = "INSERT INTO userCharData( nIndex, uID, strName, Type, Attribut, Lv, PromotionLv, RevolutionLv, Pos, PosType, strImage, AI, AtDist, AtDelay, MoveSpeed, BattleDeck, Exp ) VALUES("
				+ dat.nIndex.ToString () + "," 
				+ dat.uID.ToString() + ","
				+ "'" + dat.strImage + "'" + ","
				+ dat.nType.ToString() + ","
				+ nDamageType.ToString() + ","
				+ dat.nLevel.ToString() + ","
				+ dat.nElevatLv.ToString() + ","
				+ dat.nRevolutionLv.ToString() + ","
				+ dat.nPos.ToString() + ","
				+ dat.nPosType.ToString() + ","
				+ "'" + dat.strImage + "'" + ","
				+ dat.nAI.ToString() + ","
				+ dat.fAtDist.ToString() + ","
				+ dat.fAtDelay.ToString() + ","
				+ dat.fMoveSpeed.ToString() + ","
				+ "0" + ","
				+ dat.nExp.ToString()
				+ ")";


		//Debug.Log (query);
		BasicQuery (query);

		OBJECT_STAT_DATA StatDat = GameDataManagerChar.Instance.GetCharStatData (dat.uID);
		InsertCharStatData (StatDat);

		return true;
    }

	public bool InsertCharStatData( OBJECT_STAT_DATA dat )
	{
		string query = "INSERT INTO userCharStatData( uID, Str, Dex, Inte, StrValue, DexValue, HPValue, MPValue, PowerValue, PhysicsValue, MagicValue, PhysicsAccuracyValue, MagicAccuracyValue, " +
			"PhysicsCriValue, MagicCriValue, PhysicsDefecsValue, MagicDefecsValue, PhysicsMissValue, MagicMissValue ) VALUES("
				+ dat.uID.ToString() + ","
				+ dat.nStr.ToString() + ","
				+ dat.nDex.ToString() + ","
				+ dat.nInt.ToString() + ","
				+ dat.fStrValue.ToString() + ","
				+ dat.fDexValue.ToString() + ","
				+ dat.fHPValue.ToString() + ","
				+ dat.fMPValue.ToString() + ","
				+ dat.fPowerValue.ToString() + ","
				+ dat.fPhysicsValue.ToString() + ","
				+ dat.fMagicValue.ToString() + ","
				+ dat.fPhysicsHitValue.ToString() + ","
				+ dat.fMagicHitValue.ToString() + ","
				+ dat.fPhysicsCriValue.ToString() + ","
				+ dat.fMagicCriValue.ToString() + ","
				+ dat.fPhysicsDefecsValue.ToString() + ","
				+ dat.fMagicDefecsValue.ToString() + ","
				+ dat.fPhysicsMissValue.ToString() + ","
				+ dat.fMagicMissValue.ToString()
				+ ")";
		
		BasicQuery (query);
		return true;
	}


	public List< OBJECT_DEFAULT_DATA > LoadUserCharDataList()
	{
//		List< OBJECT_DEFAULT_DATA > ObjList = new List<OBJECT_DEFAULT_DATA> ();
//
//		int [] pcID = GameDataManagerChar.Instance.GetPcIDList ();
//
//		for (int i_1 = 0; i_1 < pcID.Length; ++i_1) {
//
//			if( pcID[i_1] > 0 )
//			{
//				OBJECT_DEFAULT_DATA dat = GameDataManagerChar.Instance.GetCharInfoData (pcID[i_1]);
//				ObjList.Add( dat);
//			}
//		}
//
		List< OBJECT_DEFAULT_DATA > ObjList = new List<OBJECT_DEFAULT_DATA> ();

		BasicQuery ("SELECT* FROM userCharData");

		while (reader.Read()) 
		{
			OBJECT_DEFAULT_DATA objDat = new OBJECT_DEFAULT_DATA();
			objDat.nRow 			= reader.GetInt32( 0 );
			objDat.nIndex 			= reader.GetInt32( 1 );
			objDat.uID 				= reader.GetInt32( 2 );
			objDat.strName 			= reader.GetString( 3 );
			objDat.nType 			= reader.GetInt32( 4 );
			objDat.DamageType 		= (eDAMAGE_TYPE)reader.GetInt32( 5 );
			objDat.nLevel 			= reader.GetInt32( 6 );
			objDat.nElevatLv 		= reader.GetInt32( 7 );
			objDat.nRevolutionLv 	= reader.GetInt32( 8 );
			objDat.nPos			 	= reader.GetInt32( 9 );
			objDat.nPosType			= reader.GetInt32( 10 );
			objDat.strImage		 	= reader.GetString( 11 );
			objDat.nAI		 		= reader.GetInt32( 12 );
			objDat.fAtDist			= reader.GetFloat( 13 );
			objDat.fAtDelay			= reader.GetFloat( 14 );
			objDat.fMoveSpeed		= reader.GetFloat( 15 );
			objDat.nBattleDeck		= reader.GetInt32( 16 );
			objDat.nExp				= reader.GetInt32( 17 );
			ObjList.Add( objDat );
		}

		return ObjList;
	}

	public List< OBJECT_DEFAULT_DATA > LoadUserCharDataList( string strQuery )
	{
		List< OBJECT_DEFAULT_DATA > ObjList = new List<OBJECT_DEFAULT_DATA> ();
		
		BasicQuery (strQuery);
		
		while (reader.Read()) 
		{
			OBJECT_DEFAULT_DATA objDat = new OBJECT_DEFAULT_DATA();
			
			objDat.nRow 			= reader.GetInt32( 0 );
			objDat.nIndex 			= reader.GetInt32( 1 );
			objDat.uID 				= reader.GetInt32( 2 );
			objDat.strName 			= reader.GetString( 3 );
			objDat.nType 			= reader.GetInt32( 4 );
			objDat.DamageType 		= (eDAMAGE_TYPE)reader.GetInt32( 5 );
			objDat.nLevel 			= reader.GetInt32( 6 );
			objDat.nElevatLv 	= reader.GetInt32( 7 );
			objDat.nRevolutionLv 	= reader.GetInt32( 8 );
			objDat.nPos			 	= reader.GetInt32( 9 );
			objDat.nPos			 	= reader.GetInt32( 10 );
			objDat.strImage		 	= reader.GetString( 11 );
			objDat.nAI		 		= reader.GetInt32( 12 );
			objDat.fAtDist			= reader.GetFloat( 13 );
			objDat.fAtDelay			= reader.GetFloat( 14 );
			objDat.fMoveSpeed		= reader.GetFloat( 15 );
			objDat.nBattleDeck		= reader.GetInt32( 16 );
			objDat.nExp				= reader.GetInt32( 17 );
			ObjList.Add( objDat );
			//return objDat;
		}
		
		return ObjList;
	}


	public OBJECT_DEFAULT_DATA LoadUsercharData( int nRow )
	{
		BasicQuery ("SELECT* userCharData WHERE Row = " + nRow.ToString());

		while (reader.Read()) 
		{
			OBJECT_DEFAULT_DATA objDat = new OBJECT_DEFAULT_DATA();
			
			objDat.nRow 			= reader.GetInt32( 0 );
			objDat.nIndex 			= reader.GetInt32( 1 );
			objDat.uID 				= reader.GetInt32( 2 );
			objDat.strName 			= reader.GetString( 3 );
			objDat.nType 			= reader.GetInt32( 4 );
			objDat.DamageType 		= (eDAMAGE_TYPE)reader.GetInt32( 5 );
			objDat.nLevel 			= reader.GetInt32( 6 );
			objDat.nElevatLv 	= reader.GetInt32( 7 );
			objDat.nRevolutionLv 	= reader.GetInt32( 8 );
			objDat.nPos			 	= reader.GetInt32( 9 );
			objDat.nPos			 	= reader.GetInt32( 10 );
			objDat.strImage		 	= reader.GetString( 11 );
			objDat.nAI		 		= reader.GetInt32( 12 );
			objDat.fAtDist			= reader.GetFloat( 13 );
			objDat.fAtDelay			= reader.GetFloat( 14 );
			objDat.fMoveSpeed		= reader.GetFloat( 15 );
			objDat.nBattleDeck		= reader.GetInt32( 16 );
			objDat.nExp				= reader.GetInt32( 17 );
			return objDat;
		}
		
		return null;
	}



	public OBJECT_STAT_DATA LoadUserCharStatData( int uID )
	{
		return null;
	}
}
