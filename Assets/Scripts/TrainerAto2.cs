using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvolutionaryPerceptron.MendelMachine;


public class TrainerAto2 : MendelMachine
{
    int index = 0; //Just one way to change the generation
                   //Init all variables

    public Transform spawnPoint;
    protected override void Start()
    {
        individualsPerGeneration = 30; //You can set an individuals per generation here
        base.Start();
        StartCoroutine(InstantiateBotCoroutine());
    }
    //When a bot die
    public override void NeuralBotDestroyed(Brain neuralBot)
    {
        //Doo some cool stuff, read the examples
        Destroy(neuralBot.gameObject); //Don't forget to destroy the gameObject

        index--;
        if (index <= 0)
        {
            Save(); //don't forget to save when you change the generation
            population = Mendelization();
            generation++;
            StartCoroutine(InstantiateBotCoroutine());
        }
    }
    //You can instantiate one, two, what you want
    IEnumerator InstantiateBotCoroutine()
    {
        //Instantiate bots
        index = individualsPerGeneration;
        for (int i = 0; i < individualsPerGeneration; i++)
        {
            var b = InstantiateBot(population[i], 20, spawnPoint , i); // A way to instantiate
        }
        yield return new WaitForSeconds(200);
    }
}
