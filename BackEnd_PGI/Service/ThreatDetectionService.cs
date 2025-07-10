using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace BackEnd_PGI.Service
{
    public class ThreatDetectionService : IThreatDetectionService
    {
        private readonly ThreatDetectionRules _rules;
        private readonly ITipoIOCRepository _tipoIOCRepository;

        public ThreatDetectionService(IOptions<ThreatDetectionRules> rules, ITipoIOCRepository tipoIOCRepository)
        {
            _rules = rules.Value;
            _tipoIOCRepository = tipoIOCRepository;
        }

        public async Task<TipoIOC?> DetectThreatAndGetTipoIOCAsync(int tipoAssetId, string fileContent)
        {
            var rule = _rules.Rules.FirstOrDefault(r => r.TipoAssetID == tipoAssetId);
            if (rule == null) return null;

            foreach (var keyword in rule.Keywords)
            {
                if (Regex.IsMatch(fileContent, keyword, RegexOptions.IgnoreCase))
                {
                    return await _tipoIOCRepository.GetByKeywordAsync(keyword);
                }
            }

            return null; // Si no se detecta ninguna amenaza
        }
    }
}
