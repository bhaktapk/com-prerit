namespace Com.Prerit.Infrastructure.Routing
{
    public interface IRouteValueOptimizer
    {
        #region Methods

        object DeoptimizeRouteValue(object value);

        object OptimizeRouteValue(object value);

        #endregion
    }
}