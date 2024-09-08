using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button musicOnBtn;
    [SerializeField] private Button musicOffBtn;
    [SerializeField] private Button sfxOnBtn;
    [SerializeField] private Button sfxOffBtn;

    private void Start()
    {
        musicOnBtn.onClick.AddListener(MusicBtnOnClick);
        musicOffBtn.onClick.AddListener(MusicBtnOffClick);
        sfxOnBtn.onClick.AddListener(SfxBtnOnClick);
        sfxOffBtn.onClick.AddListener(SfxBtnOffClick);
    }

    public void PausePanelOn()
    {
        pausePanel.gameObject.SetActive(true);
    }
    
    public void PausePanelOff()
    {
        pausePanel.gameObject.SetActive(false);
    }

    public void ChangeGamePlayScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    void MusicBtnOnClick()
    {
        musicOnBtn.gameObject.SetActive(false);
        musicOffBtn.gameObject.SetActive(true);
        AudioManager.Instance.OffMusic();
    }
    
    void MusicBtnOffClick()
    {
        musicOnBtn.gameObject.SetActive(true);
        musicOffBtn.gameObject.SetActive(false);
        AudioManager.Instance.PlayMusic();
    }

    void SfxBtnOnClick()
    {
        sfxOnBtn.gameObject.SetActive(false);
        sfxOffBtn.gameObject.SetActive(true);
        AudioManager.Instance.OffSfx();
    }

    void SfxBtnOffClick()
    {
        sfxOnBtn.gameObject.SetActive(true);
        sfxOffBtn.gameObject.SetActive(false);
        AudioManager.Instance.OnSfx();
    }


}
