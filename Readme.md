# Redhotminte Mvvmcross style





## Installation

### iOS

Override the plugin configuration in your setup to set a fontsize factor. You could, depending on the device, change the overall size factor. 

	protected override IMvxPluginConfiguration 	GetPluginConfiguration(Type plugin) {
		(Mvx.Resolve<IAssetPlugin>() as TouchAssetPlugin).Setup(new RedhotminuteStyleConfiguration() {
			FontSizeFactor = sizeFactor
		});
	}

### Android

Same as with iOS. You can optionally also add a different lineheight. In some cases older Android devices really lack space. This will help.

	protected override IMvxPluginConfiguration GetPluginConfiguration(Type plugin) {
		(Mvx.Resolve<IAssetPlugin>() as DroidAssetPlugin).Setup(new RedhotminuteStyleConfiguration() {
			FontSizeFactor = fontSizeFactor,
			LineHeightFactor = lineHeightFactor
		});
	}
	
### Cross
To add assets you could setup assets at loading the plugins

	public override void LoadPlugins(MvvmCross.Platform.Plugins.IMvxPluginManager pluginManager)
	{
		base.LoadPlugins(pluginManager);
		Assets.ConfigureAssets();
	}

## Assets

### Colors

#### Setup
	var plugin = Mvx.Resolve<IAssetPlugin>();
plugin.AddColor(new MvxColor(0, 139, 178), "BlueDark")

Viewmodels :

	[MvxInject]
	public IAssetPlugin AssetProvider { get; set; }

#### Binding iOS

#### Binding Android

	<LinearLayout
    	android:layout_width="match_parent"
    	android:layout_height="1dp"
	    local:MvxBind="BackgroundColor AssetProvider,Converter=AssetColor,ConverterParameter=BlueDark" />

### Fonts

#### Setup

	plugin.AddFont(new BaseFont() { Name = "H1", 	FontFilename = "Awesome-font.ttf", FontPlatformName = "Awesome", Size = 16,Color = plugin.GetColor("PrimaryBase")});
	
#### Binding iOS

#### Binding Android


