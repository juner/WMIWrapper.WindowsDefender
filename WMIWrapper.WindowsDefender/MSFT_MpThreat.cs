using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WMIWrapper.WindowsDefender
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/dn439469(v=vs.85).aspx">MSFT_MpThreat class</a>
    /// </summary>
    public class MSFT_MpThreat : WMIWrapperBase
    {
        protected MSFT_MpThreat(ManagementObject mo) : base(mo) { }
        public static IEnumerable<MSFT_MpThreat> SearchAll()
        {
            var scope = new ManagementScope(@"\\.\Root\Microsoft\Windows\Defender");
            var path = new ManagementPath(nameof(MSFT_MpThreat));
            var options = new ObjectGetOptions();
            using (var c = new ManagementClass(scope, path, options))
            using (var collections = c.GetInstances())
                foreach (ManagementObject mo in collections)
                    yield return new MSFT_MpThreat(mo);
        }
        public static MSFT_MpThreat SearchByThreatID(long ThreatID)
        {
            var path = new ManagementPath(nameof(MSFT_MpThreat));
            var scope = new ManagementScope(@"\\.\Root\Microsoft\Windows\Defender");
            var query = $"SELECT * FROM {nameof(MSFT_MpThreat)} WHERE {nameof(ThreatID)} = {ThreatID}";
            var options = new EnumerationOptions();
            using (var seracher = new ManagementObjectSearcher())
            {
                seracher.Scope = scope;
                seracher.Query.QueryString = query;
                using (var collection = seracher.Get())
                    foreach (ManagementObject mo in collection)
                        return new MSFT_MpThreat(mo);
                return null;
            }
        }
        /// <summary>
        /// The Schema Version
        /// </summary>
        public string SchemaVersion => GetProperty(nameof(SchemaVersion), "1.0.0.0");
        /// <summary>
        /// Unique Detection ID
        /// </summary>
        public long ThreatID => GetProperty<long>(nameof(ThreatID));
        /// <summary>
        /// The name of the threat
        /// </summary>
        public string ThreatName => GetProperty<string>(nameof(ThreatName));
        public MpThreatSeverityID SeverityID => GetProperty<MpThreatSeverityID>(nameof(SeverityID));
        public MpThreatCategoryID CategoryID => GetProperty<MpThreatCategoryID>(nameof(CategoryID));
        public MpThreatTypeID TypeID => GetProperty<MpThreatTypeID>(nameof(TypeID));
        /// <summary>
        /// Threat Rollup Status
        /// </summary>
        public uint RollupStatus => GetProperty<uint>(nameof(RollupStatus));
        /// <summary>
        /// List of resources affected by the threat
        /// </summary>
        public string[] Resources => GetProperty(nameof(Resources), (string[])Enumerable.Empty<string>());
        /// <summary>
        /// Specifies if threat has executed
        /// </summary>
        public bool DidThreatExecute => GetProperty(nameof(DidThreatExecute), false);
        /// <summary>
        /// Specifies if the threat is active
        /// </summary>
        public bool IsActive => GetProperty(nameof(IsActive), false);
        public void Remove()
        {
            using (var parameters = mo.GetMethodParameters(nameof(Remove)))
            {
                var options = new InvokeMethodOptions();
                mo.InvokeMethod(nameof(Remove), parameters, options);
            }
        }
        public override string ToString() => $"{nameof(MSFT_MpThreat)}{{{nameof(SchemaVersion)}:{SchemaVersion}, {nameof(ThreatID)}:{ThreatID}, {nameof(ThreatName)}:{ThreatName}, {nameof(SeverityID)}:{SeverityID}, {nameof(CategoryID)}:{CategoryID}, {nameof(TypeID)}:{TypeID}, {nameof(RollupStatus)}:{RollupStatus}, {nameof(Resources)}:[{string.Join(" , ", Resources)}], {nameof(DidThreatExecute)}:{DidThreatExecute}, {nameof(IsActive)}:{IsActive}}}";

    }
    public enum MpThreatCategoryID : byte
    {
        INVALID = 0,
        ADWARE = 1,
        SPYWARE = 2,
        PASSWORDSTEALER = 3,
        TROJANDOWNLOADER = 4,
        WORM = 5,
        BACKDOOR = 6,
        REMOTEACCESSTROJAN = 7,
        TROJAN = 8,
        EMAILFLOODER = 9,
        KEYLOGGER = 10,
        DIALER = 11,
        MONITORINGSOFTWARE = 12,
        BROWSERMODIFIER = 13,
        COOKIE = 14,
        BROWSERPLUGIN = 15,
        AOLEXPLOIT = 16,
        NUKER = 17,
        SECURITYDISABLER = 18,
        JOKEPROGRAM = 19,
        HOSTILEACTIVEXCONTROL = 20,
        SOFTWAREBUNDLER = 21,
        STEALTHNOTIFIER = 22,
        SETTINGSMODIFIER = 23,
        TOOLBAR = 24,
        REMOTECONTROLSOFTWARE = 25,
        TROJANFTP = 26,
        POTENTIALUNWANTEDSOFTWARE = 27,
        ICQEXPLOIT = 28,
        TROJANTELNET = 29,
        FILESHARINGPROGRAM = 30,
        MALWARE_CREATION_TOOL = 31,
        REMOTE_CONTROL_SOFTWARE = 32,
        TOOL = 33,
        TROJAN_DENIALOFSERVICE = 34,
        TROJAN_DROPPER = 36,
        TROJAN_MASSMAILER = 37,
        TROJAN_MONITORINGSOFTWARE = 38,
        TROJAN_PROXYSERVER = 39,
        VIRUS = 40,
        KNOWN = 42,
        UNKNOWN = 43,
        SPP = 44,
        BEHAVIOR = 45,
        VULNERABILTIY = 46,
        POLICY = 47,
    }
    public enum MpThreatSeverityID : byte
    {
        Unknown = 0,
        Low = 1,
        Moderate = 2,
        High = 3,
        Severe = 4,
    }
    public enum MpThreatTypeID : byte
    {
        KnownBad = 0,
        Behavior = 1,
        Unknown = 2,
        KnownGood = 3,
        NRI = 4,
    }
}