namespace Core.Presentation.Models
{
    public interface IFilterable<TFilterSource>
    {
        /// <summary>
        /// Filter source object
        /// </summary>
        TFilterSource Filter { get; set; }
        /// <summary>
        /// Generate/ return a query string from the passed filterSource
        /// </summary>
        /// <param name="filterSource"></param>
        /// <returns></returns>
        string QueryString(TFilterSource filterSource);
    }
}
