using Autofac;
using Autofac.Core;
using Caliburn.Micro;

namespace CodeConcussion.KVL.Utilities.Container
{
    public class AutoSubscriptionModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Activated += OnComponentActivated;
        }

        private static void OnComponentActivated(object sender, ActivatedEventArgs<object> e)
        {
            if (e?.Instance is IHandle handler) e.Context.Resolve<IEventAggregator>().Subscribe(handler);
        }
    }
}
