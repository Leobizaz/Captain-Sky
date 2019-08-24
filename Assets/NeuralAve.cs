using UnityEngine;

namespace EvolutionaryPerceptron.Ato2.Aves
{

    [RequireComponent(typeof(BotControl))]
    public class NeuralAve : BotHandler
    {
        BotControl botControl;
        protected override void Start()
        {
            base.Start();
            botControl = GetComponent<BotControl>();
        }

        void Update()
        {
            var output = nb.SetInput(GetInputs());
            if(output[0,0] > 0.5f)
            {
                //dosomething
            }
            nb.AddFitness(Time.deltaTime);
            
        }

        double[,] GetInputs()
        {



            return new double[1, 5] { {1, 2, 3, 4, 5 } };
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Boundary"))
            {
                nb.Destroy();
            }
        }
    }
}