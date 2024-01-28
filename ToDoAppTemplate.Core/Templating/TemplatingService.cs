using System.Text;
using Fluid;
using Fluid.ViewEngine;

namespace ToDoAppTemplate.Core.Templating;

public sealed class TemplatingService
{
    private readonly IFluidViewRenderer _renderer;
    private readonly FluidViewEngineOptions _options;

    public TemplatingService(IFluidViewRenderer renderer, FluidViewEngineOptions options)
    {
        _renderer = renderer;
        _options = options;
    }

    public async Task<string> Render<T>(string template, T model)
    {
        var context = new TemplateContext(model, _options.TemplateOptions);

        var viewPath = GetViewPath(template, _options);

        var sb = new StringBuilder();
        await using var sw = new StringWriter(sb);
        await _renderer.RenderViewAsync(sw, viewPath, context);

        return sb.ToString();
    }

    private static string? GetViewPath(string template, FluidViewEngineOptions options)
    {
        var fileProvider = options.ViewsFileProvider;

        if (fileProvider is null)
        {
            throw new Exception("Views file provider is missing.");
        }

        foreach (var location in options.ViewsLocationFormats)
        {
            var viewFilename = Path.Combine(string.Format(location, template));

            var fileInfo = fileProvider.GetFileInfo(viewFilename);

            if (fileInfo.Exists)
            {
                return viewFilename;
            }
        }

        return null;
    }
}
