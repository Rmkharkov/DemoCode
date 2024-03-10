using UniRx;

namespace Battle
{
    public class DamageableController : BaseController<DamageableModel>
    {
        public ReactiveCommand GetDamaged { get; } = new ReactiveCommand();

        public void GetDamage(IDamageable damaged, IDamageable thisDamageable)
        {
            if (thisDamageable == damaged)
            {
                GetDamaged.Execute();
            }
        }
    }
}