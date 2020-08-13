﻿public interface IResourceManager
{
    float MoneyCalculationInterval { get; }
    int StartMoneyAmount { get; }
    int RemovalPrice { get; }

    void AddMoneyAmount(int amount);
    void CalculateTownIncome();
    bool CanIBuyIt(int amount);
    void SpendMoney(int amount);
    void PrepareResourceManager(BuildingManager buildingManager);
    void AddToPopulation(int amount);
    void ReducePopulation(int amount);
    void AddToShoppingCartAmount(int amount);
    void ReduceShoppingCartAmount(int amount);
    int ShoppingCartAmount();
    void ClearShoppingCartAmount();
}