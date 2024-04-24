using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

namespace Zong
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject completPanel;
        [SerializeField]
        private GameObject mainUi;
        [SerializeField]
        private TextMeshProUGUI completeText;
        [SerializeField]
        private TextMeshProUGUI pointText;
        private int playerPoint;
        [SerializeField]
        private Button replayButton;

        [SerializeField]
        private TMP_Dropdown instrumentDropdown;
        [SerializeField]
        private TMP_Dropdown weaponDropdown;
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                        instance = singletonObject.AddComponent<GameManager>();
                    }
                }
                return instance;
            }
        }
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            replayButton.onClick.AddListener(Replay);
            instance = this;
        }

        void Start()
        {
            playerPoint = PlayerPrefs.GetInt("Point", 0);
            SetupDropdown();
            mainUi.SetActive(false);
        }

        public void ShowMainUI()
        {
            mainUi.SetActive(true);
        }
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        void Update()
        {
            if (pointText != null)
            {
                pointText.text = $"Points : {playerPoint.ToString()}";
            }
        }

        public void Complete(string type)
        {
            playerPoint += 10;
            PlayerPrefs.SetInt("Point", playerPoint);
            PlayerPrefs.Save();
            completPanel.SetActive(true);
            completeText.text = $"You have Drop Stone on {type} cofin";

            Debug.Log("complete");
        }

        void OnDestroy()
        {
            replayButton.onClick.RemoveListener(Replay);
        }

        public void AddItemToInstrument(string newItem)
        {
            TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData(newItem);

            instrumentDropdown.options.Add(item);
        }

        void SetupDropdown()
        {
            List<string> options = new List<string>
            {
                "Wolf Stone",
                "Lizard Crystal",
                "Crush Skull Steel"
            };

            weaponDropdown.AddOptions(options);

            instrumentDropdown.captionText.text = "Instrument";
            weaponDropdown.captionText.text = "Weapon";
        }
        


    }
}
