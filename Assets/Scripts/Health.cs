using System;

public class Health
{
    private readonly float _maxAmount;

    private float _amount;

    public Health(float maxAmount)
    {
        _amount = maxAmount;
        _maxAmount = maxAmount;
    }

    public event Action Changed;

    public float Amount => _amount;

    public void SetAmount(float amount)
    {
        if (amount > _maxAmount || amount < 0)
            throw new Exception("Invalid health amount to set.");

        _amount = amount;
        Changed?.Invoke();
    }

    public void ApplyHeal(float amountToAdd)
    {
        float currentHealth = _amount + amountToAdd;

        if (currentHealth > _maxAmount)
            _amount = _maxAmount;
        else
            _amount = currentHealth;

        Changed?.Invoke();
    }

    public void ApplyDamage(float amountToSubtract)
    {
        float currentHealth = _amount - amountToSubtract;

        if (currentHealth < 0)
            _amount = 0;
        else
            _amount = currentHealth;

        Changed?.Invoke();
    }
}
