using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace IOTFinalServer.ViewModel
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OrderViewModel>();
            SimpleIoc.Default.Register<PositionViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public OrderViewModel Order
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderViewModel>();
            }
        }

        public PositionViewModel Position
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PositionViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
