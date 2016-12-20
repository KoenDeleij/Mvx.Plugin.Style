using System;
namespace Redhotminute.Plugin.Style {
	//Bit of a hack to make it possible to bind fonts in listitems
	public class Stylable :IStylable {
		public string Empty {
			get; set;
		} = string.Empty;
	}

	public interface IStylable {
		string Empty { get; set; }
	}
}
