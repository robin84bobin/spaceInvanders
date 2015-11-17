
using System;

public class LoadEventArgs : EventParam
{
	private string _message;
	public string message {
		get {
			return _message;
		}
	}

	public LoadEventArgs (string message)
	{
		_message = message;
	}
}


