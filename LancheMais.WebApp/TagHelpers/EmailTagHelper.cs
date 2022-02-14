using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LancheMais.WebApp.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string Endereco { get; set; }
        public string Conteudo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "malito:" + Endereco);
            output.Content.SetContent(Conteudo);
        }
    }
}
