# Redhotminute MvvmCross Style Plugin



# Initialization

## iOS

Override the plugin configuration in your setup to set a fontsize factor. You could, depending on the device, change the overall size factor. 

	protected override IMvxPluginConfiguration 
		if (plugin == typeof(Redhotminute.Mvx.Plugin.Style.Touch.Plugin)) {
			return new RedhotminuteStyleConfiguration() {
				FontSizeFactor = sizeFactor
			};
		}
	}

## Android

Same as with iOS. You can optionally also add a different lineheight. In some cases older Android devices really lack space. This will help.

	protected override IMvxPluginConfiguration 
		if (plugin == typeof(Redhotminute.Mvx.Plugin.Style.Droid.Plugin)) {
			return new RedhotminuteStyleConfiguration() {
				FontSizeFactor = fontSizeFactor,
				LineHeightFactor = lineHeightFactor
			};
		}
	}
	
If you want to use the MvxFont binding in layout files, you'll need to add these lines of code in the setup :

	protected override MvvmCross.Binding.Droid.MvxAndroidBindingBuilder CreateBindingBuilder() 	{
		var bindingBuilder = new MvxAndroidStyleBindingBuilder();
		return bindingBuilder;
	}

	
## Cross
To add assets you can resolve the asset plugin :

	public override void LoadPlugins(MvvmCross.Platform.Plugins.IMvxPluginManager pluginManager)
	{
		base.LoadPlugins(pluginManager);
		var plugin = Mvx.Resolve<IAssetPlugin>();
		//add your fonts here. of course wouldn't actually add them here, but after the plugins are loaded you could.
	}

To be able to bind directly in your views, you'll have to add the AssetProvider to your viewmodel :

	[MvxInject]
	public IAssetPlugin AssetProvider { get; set; }

# Colors

## Cross

### Setup
	var plugin = Mvx.Resolve<IAssetPlugin>();
	plugin.AddColor(new MvxColor(0, 139, 178), "BlueDark")


## iOS

	bindingSet.Bind(View).For(v => v.BackgroundColor).To(vm => 	vm.AssetProvider).WithConversion("AssetColor", "BlueDark");

## Android

	<LinearLayout
    	android:layout_width="match_parent"
    	android:layout_height="match_parent"
	    local:MvxBind="BackgroundColor AssetProvider,Converter=AssetColor,ConverterParameter=BlueDark" />

# Fonts

## Features

This plugin allows you to add fonts. Each font contains the following properties :

**BaseFont**

* **Name**, *String* (Used for reference when binding)
* **FontFilename**, *String* (the full filename of the font, like "Arial.ttf")
* **FontPlatformName**, *String* (this is mainly for iOS. iOS doesn't refer to fonts with the actual file name but by a tag. You can find it's name in the FontBook under PostScript Name)
* **FontPlatformSize**, *float* (Automatically set based on the FontSizeFactor per platform. You don't have to set this)
* **Size**, *int* (Size of the font)  

For buttons and more advanced styling you can use the Font class

**Font**

* **BoldFont**, *IBaseFont* (Font used when parts of the bound text are marked to be bold (with *)
* **SelectedColor**, *MvxColor* (Font color shown when a button is set to selected. Selected property for iOS, state_selected for Android)
* **DisabledColor**, *MvxColor* (Font color shown when a button is disabled. Enabled false for iOS, state_enabled false for Android)
* **LineHeight**, *int* (Only available for labels and textviews. For iOS you'll need to use the AttributedText converter)
* **Alignment**, *TextAlignment* (Left/Center/Right, you'll get it)

## Cross

### Setup
You can add fonts like this:

	plugin.AddFont(new Font() { Name = "H1", 	FontFilename = "Awesome-font.ttf", FontPlatformName = "Awesome", Size = 16,Color = plugin.GetColor("BlueDark")});
	
## iOS

### Setup

To be able to access your custom fonts, add the font files to Resources/Fonts. 
You'll also have to add an entry to your Info.plist.
Add an Array call 'UIAppFonts'.
For each font, add 1 string entry to the array with the path to your font (in this case it will be "Fonts/Awesome-font.otf"

For all fonts you'll have to fill the FontPlatformName property. iOS doesn't refer to fonts with the actual file name but by a tag. You can find it's name in the FontBook under PostScript Name.

### UIViewController

To bind a font only, you can use :

	this.BindFont(Button, "H1");
	this.BindFont(Label, "H1");
	
It's available for **UIButton**,  **UILabel** and **UITextField**

When you're using the BindFont binding, it will not setup your line height and allignment. 
In order to do this use :

	bindingSet.Bind(Label).For(v => v.AttributedText).To(vm => vm.Label).WithConversion("AttributedFontText", "H1");
	
You can also mark part of the text bold by adding * * in the bound string. The plugin will look for a boldfont within the referenced font. For example:

	"This is *bold* text"

"This is" will be the regular font, "bold" will be set in the bold font (set in the Font object).


In case you're using Babel MvvmCross resource loader you can use :

	this.BindLanguageFont(Label, "LanguageKey", "H1");

### UITableViewCell

For UITableViewCells you can only use :

	bindingSet.Bind(Label).For(v => v.AttributedText).To(vm => vm.Label).WithConversion("AttributedFontText", "H1");

## Android

### Setup

To be able to access your custom fonts. Add them in your Droid project under Assets/Fonts.

### Activity

To bind a font you can use :

	<Button
		android:layout_width="wrap_content"
		android:layout_height="wrap_content"
		local:MvxFont="Font H1" />

You can combine this with the MvxLang binding. It's available for **TextView**, **EditText** and **Button**.

### Cells

To bind a font within a cell you can use :

	<TextView
		android:layout_width="wrap_content"
		android:layout_height="wrap_content"
		android:layout_gravity="center_vertical"
		local:MvxBind="Font Empty,Converter=FontResource,ConverterParameter=H1" />

Note the *Empty* object to bind to. For now you'll just need a string or object thats empty to bind to in your model. To force this, you can use the IStylable interface.

### Bold font

If you want to use the bold font for android, you'll have to use a converter :

	<TextView
		android:layout_width="match_parent"
		android:layout_height="wrap_content"
		local:MvxBind="AttributedText ViewModelAttributedText,Converter=AttributedBold,ConverterParameter='H1'"
		local:MvxFont="Font H1" />

Note that in this case H1 must have a BoldFont property added.