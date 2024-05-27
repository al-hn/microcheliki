using UnityEngine;

public class FinanceManager : MonoBehaviour
{
    public static FinanceManager Instance;

    public int money = 1000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanAfford(int amount)
    {
        return money >= amount;
    }

    public bool SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
}
