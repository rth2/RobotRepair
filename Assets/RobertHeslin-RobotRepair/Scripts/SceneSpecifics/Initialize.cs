using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LoLSDK;
using SimpleJSON;
using System.IO;


namespace RobertHeslin.RobotRepair
{
    public class Initialize : MonoBehaviour
    {
        enum LoLDataType
        {
            START = 0,
            LANGUAGE = 1 << 0
        }

        // Use to determine when all data is preset to load to next state.
        // This will protect against async request race conditions in webgl.
        LoLDataType _receivedData;

        // This should represent the data you're expecting from the platform.
        // Most games are expecting 2 types of data, Start and Language.
        LoLDataType _expectedData = LoLDataType.START | LoLDataType.LANGUAGE;

        // Relative to Assets /StreamingAssets/
        private const string languageJSONFilePath = "language.json";
        private const string startGameJSONFilePath = "startGame.json";
        // Application.streamingAssetsPath gets the path up to Assets/StreamingAssets/


        void Awake()
        {
            // Create the WebGL (or mock) object.
#if UNITY_EDITOR
            ILOLSDK webGL = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
            ILOLSDK webGL = new LoLSDK.WebGL();
#endif

            //Initialize the object, passing in the WebGL
            LOLSDK.Init(webGL, Application.identifier);
            //This results in a debug.log of "Get player activity ID"

            //register event handlers
            LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(HandleStartGame);
            LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(HandleLanguageDefs);
            LOLSDK.Instance.GameStateChanged += new GameStateChangedHandler(HandleGameStateChange);


            //mock the platform-to-game messages when in the unity editor
#if UNITY_EDITOR
            LoadMockData();
#endif

            //tells the platform that the game is ready
            LOLSDK.Instance.GameIsReady();
            StartCoroutine(_WaitForData());
        }

        IEnumerator _WaitForData()
        {
            Debug.Log($"waiting for data");
            yield return new WaitUntil(() => (_receivedData & _expectedData) != 0);
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }

        void HandleStartGame(string json)
        {
            SharedData.StartGameData = JSON.Parse(json);
            _receivedData |= LoLDataType.START;
        }

        void HandleLanguageDefs(string json)
        {
            JSONNode langDefs = JSON.Parse(json);

            // Example of accessing language strings
            //Debug.Log($"Example of accessing language strings");
            //Debug.Log(langDefs);
            //Debug.Log(langDefs["welcome"]);

            SharedData.LanguageDefs = langDefs;
            _receivedData |= LoLDataType.LANGUAGE;
        }

        void HandleGameStateChange(GameState gameState)
        {
            // Either GameState.Paused or GameState.Resumed
            Debug.Log($"HandleGameStateChange");
        }

        void LoadMockData()
        {
#if UNITY_EDITOR
            //Load Dev Language File from StreamingAssets

            string startDataFilePath = Path.Combine(Application.streamingAssetsPath, startGameJSONFilePath);
            string langCode = "en";

            if(File.Exists(startDataFilePath))
            {
                string startDataAsJSON = File.ReadAllText(startDataFilePath);
                JSONNode startGamePayLoad = JSON.Parse(startDataAsJSON);
                langCode = startGamePayLoad["languageCode"];
                HandleStartGame(startDataAsJSON);
            }

            //load dev language file from streaming assets
            string langFilePath = Path.Combine(Application.streamingAssetsPath, languageJSONFilePath);

            if(File.Exists(langFilePath))
            {
                string langDataAsJSON = File.ReadAllText(langFilePath);
                // The dev payload in language.json includes all languages.
                // Parse this file as JSON, encode, and stringify to mock
                // the platform payload, which includes only a single language.
                JSONNode langDefs = JSON.Parse(langDataAsJSON);
                // use the languageCode from startGame.json captured above
                HandleLanguageDefs(langDefs[langCode].ToString());
            }
            

#endif
        }

    } // end class
} //end namespace

