using System;

namespace Rhaeo.Agenda.ViewModels
{
  public sealed class ErrorViewModel
  {
    #region Constructors

    public ErrorViewModel(Exception exception)
    {
      Exception = exception;
    }

    #endregion

    #region Properties

    public Exception Exception { get; }

    #endregion
  }
}
