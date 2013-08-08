using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Dicom {
	public class DicomUIDGenerator {
		private static volatile DicomUID _instanceRootUid = null;
		private static DicomUID InstanceRootUID {
			get {
				if (_instanceRootUid == null) {
					lock (GenerateUidLock) {
						if (_instanceRootUid == null) {
#if !SILVERLIGHT
							NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
							for (int i = 0; i < interfaces.Length; i++) {
								if (NetworkInterface.LoopbackInterfaceIndex != i && interfaces[i].OperationalStatus == OperationalStatus.Up) {
									string hex = interfaces[i].GetPhysicalAddress().ToString();
									if (!String.IsNullOrEmpty(hex)) {
										try {
											long mac = long.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
											return DicomUID.Append(DicomImplementation.ClassUID, mac);
										} catch {
										}
									}
								}
							}
#endif
							_instanceRootUid = DicomUID.Append(DicomImplementation.ClassUID, Environment.TickCount);
						}
					}
				}
				return _instanceRootUid;
			}
			set {
				_instanceRootUid = value;
			}
		}

		private static long LastTicks = 0;
		private static object GenerateUidLock = new object();
		private static DateTime Y2K = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private Dictionary<string, DicomUID> _uidMap = new Dictionary<string, DicomUID>();

		public DicomUID Generate(DicomUID sourceUid = null) {
			lock (GenerateUidLock) {
				DicomUID destinationUid;
				if (sourceUid != null && _uidMap.TryGetValue(sourceUid.UID, out destinationUid))
					return destinationUid;

				long ticks = DateTime.UtcNow.Subtract(Y2K).Ticks;
				while (ticks == LastTicks) {
					Thread.Sleep(1);
					ticks = DateTime.UtcNow.Subtract(Y2K).Ticks;
				}
				LastTicks = ticks;

				string str = ticks.ToString();
				if (str.EndsWith("0000"))
					str = str.Substring(0, str.Length - 4);

				StringBuilder uid = new StringBuilder();
				uid.Append(InstanceRootUID.UID).Append('.').Append(str);

				destinationUid = new DicomUID(uid.ToString(), "SOP Instance UID", DicomUidType.SOPInstance);

				if (sourceUid != null)
					_uidMap.Add(sourceUid.UID, destinationUid);

				return destinationUid;
			}
		}

		public void RegenerateAll(DicomDataset dataset) {
			foreach (var ui in dataset.Where(x => x.ValueRepresentation == DicomVR.UI).ToArray()) {
				var uid = dataset.Get<DicomUID>(ui.Tag);
				if (uid.Type == DicomUidType.SOPInstance || uid.Type == DicomUidType.Unknown)
					dataset.Add(ui.Tag, Generate(uid));
			}

			foreach (var sq in dataset.Where(x => x.ValueRepresentation == DicomVR.SQ).Cast<DicomSequence>().ToArray()) {
				foreach (var item in sq) {
					RegenerateAll(item);
				}
			}
		}
	}
}
