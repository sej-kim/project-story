using UnityEngine;
using System.IO;
using System.Collections.Generic;
using STORY_ENUM;

public class CSVParser : MonoBehaviour
{
    public delegate int Parsingfun_Deleage(string[] strPath);
    public Dictionary<string, Parsingfun_Deleage> _ParsingDeleage;

	protected FileInfo _sourceFile	= null;
	protected StreamReader _reader	= null;
	protected string[] _Header		= null;


    //public virtual int StoryTextDataParse(string[] inputData) { return 0; }

	// Getter / Setter

	// Default Functions
	public void LoadFile( string filePath, ePARSE_FUN_NAME eFName )
	{
       // string fileFullPath = TextAsset.
        //Debug.Log(filePath);
		
        /*
        //string fileFullPath = Application.dataPath + "/06.Data/" + filePath;
        TextAsset texAsset = Resources.Load("Data/" + filePath) as TextAsset;

        string fileFullPath = texAsset.text;

		_sourceFile = new FileInfo( fileFullPath );
		if( _sourceFile != null && _sourceFile.Exists )
		{
			_reader = _sourceFile.OpenText();
		}

        if( _reader == null )
		{
			Debug.LogError( "File not found or not readable : " + fileFullPath );
		}

		if( _reader == null )
		{
			Debug.LogError( "LoadFile Fail : " + filePath );
			return;
		}

        int lineCount = 0;
        string inputData = _reader.ReadLine();


		while( inputData != null )
		{
			//Debug.Log( "Parsing : " + inputData );

            inputData = _reader.ReadLine();

            if (inputData == null)
            {
                break;
            }

            //Debug.Log("Parsing : " + inputData);

			string[] stringList = inputData.Split( ',' );

			if( stringList.Length == 0 )
			{
				continue;
			}

			//string keyValue = stringList[0];
            if (ParseData(stringList, lineCount, eFName) == false)
			{
				Debug.LogError( "Parsing fail : " + stringList.ToString() );
			}
			lineCount++;
		}

		_sourceFile = null;

		_reader.Dispose();
		_reader		= null;
         * */

		//Debug.Log(  "PathName : " + filePath );

        TextAsset texAsset = Resources.Load("Data/" + filePath) as TextAsset;

		//Debug.Log( texAsset.text );


        string[] fileFullPath = texAsset.text.Split('\n');



       // Debug.Log(  "texAsset" + fileFullPath.Length.ToString());
        
        int lineCount = 0;

        for (int i_1 = 1; i_1 < fileFullPath.Length; ++i_1)
        {
            string[] stringList = fileFullPath[i_1].Split(',');

            if (stringList.Length <= 1)
            {
                continue;
            }


            //string keyValue = stringList[0];
            if (ParseData(stringList, lineCount, eFName) == false)
            {
                Debug.LogError("Parsing fail : " + stringList.ToString());
            }

            ++lineCount;
         }
	}

	public bool ParseData( string[] inputData, int lineCount, ePARSE_FUN_NAME eFName )
	{
		if( lineCount == 0 )
		{
			// Header
			_Header = inputData;
		}

		if( VarifyKey( inputData[0] ) == false )
		{
			Debug.Log( "VarifyKey fail : " + inputData[0] );
			return false;
		}

        //Debug.Log("VarifyKey fail : " + inputData[0]);


        _ParsingDeleage[eFName.ToString()].Invoke(inputData);

		return true;
	}

	public virtual bool VarifyKey( string keyValue )
	{
		return true;
	}



}