namespace Core.Intrerfaces
{
    public interface IDamageable
    {

        void TakeDamage(int damage); // Получение урона
        void DestroyEntity(); // Полное уничтожение
        void ReturnToPool(); // Возвращение объекта в пул
    }
}