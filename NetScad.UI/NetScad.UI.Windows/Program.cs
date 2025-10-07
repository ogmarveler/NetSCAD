using System;
using System.Linq;
using System.Threading;

using Avalonia;
using Avalonia.ReactiveUI;


namespace NetScad.UI.Desktop
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static int Main(string[] args)
        {
            var builder = BuildAvaloniaApp()
            .UseSkia() // Skia rendering
            .UsePlatformDetect() // Auto-selects X11, Wayland, etc., on Linux
            .With(new SkiaOptions()) // Limit GPU memory usage
            .With(new Win32PlatformOptions { RenderingMode = [Win32RenderingMode.AngleEgl, Win32RenderingMode.Wgl, Win32RenderingMode.Software] }) // Enable GPU on Windows
            .With(new MacOSPlatformOptions { ShowInDock = true, }) // Options on macOS
            .With(new X11PlatformOptions { RenderingMode = [X11RenderingMode.Glx, X11RenderingMode.Software], OverlayPopups = true, UseDBusMenu = true, WmClass = AppDomain.CurrentDomain.FriendlyName, }) // Enable GPU on Linux
            .WithInterFont() // Use Inter font by default
            .LogToTrace() // Enable logging to Visual Studio output window
            .UseReactiveUI(); // MVVM framework

            if (args.Contains("--drm"))
            {
                SilenceConsole();

                // If Card0, Card1 and Card2 all don't work. You can also try:                 
                // return builder.StartLinuxFbDev(args);
                // return builder.StartLinuxDrm(args, "/dev/dri/card1");
                return builder.StartLinuxDrm(args, "/dev/dri/card1", 1D);
            }
            return builder.StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
            .UseSkia() // Skia rendering
            .UsePlatformDetect() // Auto-selects X11, Wayland, etc., on Linux
            .With(new SkiaOptions()) // Limit GPU memory usage
            .With(new Win32PlatformOptions { RenderingMode = [Win32RenderingMode.AngleEgl, Win32RenderingMode.Wgl, Win32RenderingMode.Software] }) // Enable GPU on Windows
            .With(new MacOSPlatformOptions { ShowInDock = true, }) // Options on macOS
            .With(new X11PlatformOptions { RenderingMode = [X11RenderingMode.Glx, X11RenderingMode.Software], OverlayPopups = true, UseDBusMenu = true, WmClass = AppDomain.CurrentDomain.FriendlyName, }) // Enable GPU on Linux
            .WithInterFont() // Use Inter font by default
            .LogToTrace() // Enable logging to Visual Studio output window
            .UseReactiveUI(); // MVVM framework

        private static void SilenceConsole()
        {
            new Thread(() =>
            {
                Console.CursorVisible = false;
                while (true)
                    Console.ReadKey(true);
            })
            { IsBackground = true }.Start();
        }
    }
}
