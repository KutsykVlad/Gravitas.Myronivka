using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gravitas.Platform.Web.Manager.Report
{
    public class ReportTool : IReportTool
    {
        public string ReplaceTokens(string template, object vm)
        {
            var matches = Regex.Matches(template, @"##\S+\s\S+##")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            for (var i = 0; i < matches.Length; i++)
            {
                var matchItem = matches[i].Trim('#').Split(' ');
                var selector = matchItem[0];
                var selectorValue = matchItem[1];

                var toInsert = string.Empty;
                var toReplace = matches[i];
                switch (selector)
                {
                    case "field":
                        toInsert = GetPropValue(vm, selectorValue).ToString();
                        break;
                    case "forEach":
                        i = Array.IndexOf(matches, $@"##forEachEnd {selectorValue}##");
                        if (!(GetPropValue(vm, selectorValue) is IEnumerable<object> dataItems)) break;

                        var regex = new Regex($@"##forEach {selectorValue}##(.+)##forEachEnd {selectorValue}##",
                            RegexOptions.Singleline);
                        var forEachTemplate = regex.Match(template).Groups[1].Value;

                        toInsert = dataItems.Aggregate(toInsert, (current, item) => current + ReplaceTokens(forEachTemplate, item));
                        toReplace = regex.ToString().Replace(@"(.+)", forEachTemplate);
                        break;
                    default:
                        toInsert = string.Empty;
                        break;
                }

                template = Regex.Replace(template, toReplace, toInsert);
            }

            return template;
        }

        private static object GetPropValue(object src, string propName)
        {
            var propPath = propName.Split('.');
            src = propPath.Aggregate(src, (current, prop) => current?.GetType().GetProperty(prop)?.GetValue(current, null));

            return src ?? "";
        }
    }
}