using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_SG : MonoBehaviour
{
    public Dog_SG[] dogs;
    public Cat_SG cat;
    public Transform foods;
    public Text Score_txt;
    public bool energyEaten;
    public GameObject winScene;
    public GameObject loseScene;
    public GameObject Joystick;
    public Text Score_win;
    public Text Score_lose;
    public bool DeleteDogEnergy;
    public bool AddDogEnergy;
    public bool StopDogEnergy;
    //public float speed = 8.0f;

    public int score;

    public int lives { get; private set; }


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        score = 0;
        SetScore(score);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform foods in this.foods)
        {
            foods.gameObject.SetActive(true);
        }
        for (int i = 0; i < 4; i++)
        {
            if (dogs[i].gameObject.name == "Dog_Red_SG")
            {
                dogs[i].transform.position = new Vector3(-12.5f, 14.42f, -5f);
            }
            else if (dogs[i].gameObject.name == "Dog_Green_SG")
            {
                dogs[i].transform.position = new Vector3(-12.4f, -14.46f, -5f);
            }
            else if (dogs[i].gameObject.name == "Dog_Blue_SG")
            {
                dogs[i].transform.position = new Vector3(10.9f, 14.58f, -5f);
            }
            else if (dogs[i].gameObject.name == "Dog_Yellow_SG")
            {
                dogs[i].transform.position = new Vector3(10.65f, -14.42f, -5f);
            }
            dogs[i].gameObject.SetActive(true);
        }
        RespawnCat();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.dogs.Length; i++)
        {
            this.dogs[i].gameObject.SetActive(false);
        }
        this.cat.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        Score_txt.text = "Score : " + score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void DogEaten(Dog_SG dog)
    {
        SoundManager_SG.PlaySound("DogDied");
        dog.gameObject.SetActive(false);
        score += dog.points;
        SetScore(score);
        StartCoroutine(RespawnDog(dog));
    }

    public void CatEaten()
    {
        SoundManager_SG.PlaySound("CatDied");
        this.cat.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke("RespawnCat", 2.0f);
        }
        else
        {
            Case("lose");
        }
    }

    public void NormalFoodEaten(NormalFood_SG normal_food)
    {
        normal_food.gameObject.SetActive(false);
        score += normal_food.points;
        SetScore(score);

        if (CheckingAllFoods())
        {
            SoundManager_SG.PlaySound("Victory");
            this.cat.gameObject.SetActive(false);
            Case("win");
        }
    }

    public void CatEnergyEaten(CatEnergy_SG cat_energy)
    {
        SoundManager_SG.PlaySound("CatEnergyEaten");
        NormalFoodEaten(cat_energy);
        energyEaten = true;
        Invoke("ResetEnergy", 8.0f);
    }

    private bool CheckingAllFoods()
    {
        foreach (Transform foods in this.foods)
        {
            if (foods.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void ResetEnergy()
    {
        energyEaten = false;
    }

    private IEnumerator RespawnDog(Dog_SG dog)
    {
        yield return new WaitForSeconds(5);
        if (dog.gameObject.name == "Dog_Red_SG")
        {
            dog.transform.position = new Vector3(-12.5f, 14.42f, -5f);
        }
        else if (dog.gameObject.name == "Dog_Green_SG")
        {
            dog.transform.position = new Vector3(-12.4f, -14.46f, -5f);
        }
        else if (dog.gameObject.name == "Dog_Blue_SG")
        {
            dog.transform.position = new Vector3(10.9f, 14.58f, -5f);
        }
        else if (dog.gameObject.name == "Dog_Yellow_SG")
        {
            dog.transform.position = new Vector3(10.65f, -14.42f, -5f);
        }
        dog.gameObject.SetActive(true);
    }

    public void RespawnCat()
    {
        this.cat.transform.position = new Vector3(-0.37f, -0.72f, -5f);
        this.cat.gameObject.SetActive(true);
    }

    private void Case(string casename)
    {
        Time.timeScale = 0f;
        Joystick.SetActive(false);
        if (casename == "win")
        {
            Debug.Log("win");
            winScene.SetActive(true);
            Score_win.text = "Score : " + score;
        }
        else if (casename == "lose")
        {
            Debug.Log("Lose");
            loseScene.SetActive(true);
            Score_lose.text = "Score : " + score;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        Joystick.SetActive(true);
        Time.timeScale = 1f;
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
        Joystick.SetActive(true);
        Time.timeScale = 1f;
    }
    public void DeleteDogEnergyEat(DeleteDogEnergy deleteDogEnergy)
    {
        SoundManager_SG.PlaySound("CatEnergyEaten");
        dogs[0].gameObject.SetActive(false);
        NormalFoodEaten(deleteDogEnergy);
        DeleteDogEnergy = true;
        Invoke("ResetDelete", 6.0f);
    }
    public void AddDogEnergyEat(AddDogEnergy addDogEnergy)
    {
        SoundManager_SG.PlaySound("CatEnergyEaten");
        dogs[4].gameObject.SetActive(true);
        NormalFoodEaten(addDogEnergy);
        AddDogEnergy = true;
        Invoke("ResetAdd", 6.0f);
    }
    public void StopDogEnergyEat(StopDogEnergy stopDogEnergy)
    {
        SoundManager_SG.PlaySound("CatEnergyEaten");
        dogs[0].movement.speed = 0f;
        dogs[1].movement.speed = 0f;
        dogs[2].movement.speed = 0f;
        dogs[3].movement.speed = 0f;
        dogs[4].movement.speed = 5f;
        NormalFoodEaten(stopDogEnergy);
        StopDogEnergy = true;
        Invoke("ResetStop", 3.0f);
    }
    public void ResetDelete()
    {
        dogs[0].gameObject.SetActive(true);
    }
    public void ResetAdd()
    {
        dogs[4].gameObject.SetActive(false);
    }
    public void ResetStop()
    {
        dogs[0].movement.speed = 5f;
        dogs[1].movement.speed = 5f;
        dogs[2].movement.speed = 5f;
        dogs[3].movement.speed = 5f;
        dogs[4].movement.speed = 5f;
    }
}