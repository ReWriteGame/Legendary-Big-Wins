using UnityEngine;
using UnityEngine.Events;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Arrow arrow1;
    [SerializeField] private Arrow arrow2;
    [SerializeField] private Arrow arrow3;
    [SerializeField] private Arrow arrow4;
    [SerializeField] private Roulette roulette;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private ScoreCounter scoreBid;
    private int endRoulette = 0;

   

    public UnityEvent endGamaEvent;

    public void EndSpinRoulette()
    {
        endRoulette++;
        GetResults();
    }

    private void TakeAwayBid()
    {
        scoreCounter.TakeAway(scoreBid.Score);
    }

    private void OnEnable()
    {
        roulette.startRotateEvent?.AddListener(TakeAwayBid);
    }

    private void OnDisable()
    {
        roulette.startRotateEvent?.RemoveListener(TakeAwayBid);

    }

    public void GetResults()
    {
        if (endRoulette == 4)
        {
            int[] slotsValue = new int[5]{0,0,0,0,0};

            slotsValue[arrow1.collidedObject.ID - 1]++;
            slotsValue[arrow2.collidedObject.ID - 1]++;
            slotsValue[arrow3.collidedObject.ID - 1]++;
            slotsValue[arrow4.collidedObject.ID - 1]++;

            foreach (int value in slotsValue)
                if(value > 1)scoreCounter.Add(scoreBid.Score * value);
                
            endRoulette = 0;
            endGamaEvent?.Invoke();
        }
      
    }

}
