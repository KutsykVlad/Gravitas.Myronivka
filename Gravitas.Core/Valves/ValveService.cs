using System.Collections.Generic;
using System.Linq;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;

namespace Gravitas.Core.Valves
{
    public class ValveService : IValveService
    {
        private readonly Dictionary<long, List<Valve>> _valvesByNodeId = new Dictionary<long, List<Valve>>
        {
            {
                (long) NodeIdValue.UnloadPoint20, new List<Valve>
                {
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 1 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000402,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = false
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 2 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000402,
                                ExpectedResult = false
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 3 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000404,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000501,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000502,
                                ExpectedResult = false
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 4 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000404,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000501,
                                ExpectedResult = false
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000502,
                                ExpectedResult = true
                            }
                        }
                    }
                }
            },
            {
                (long) NodeIdValue.UnloadPoint21, new List<Valve>
                {
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 1 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000402,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = false
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 2 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000402,
                                ExpectedResult = false
                            },
                            new ValveDevice
                            {
                                DeviceId = 22000403,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 3 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000404,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000501,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000502,
                                ExpectedResult = false
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №2 Силос 4 Загрузка",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 22000404,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000501,
                                ExpectedResult = false
                            },
                            new ValveDevice
                            {
                                DeviceId = 8000502,
                                ExpectedResult = true
                            }
                        }
                    }
                }
            },
            {
                (long) NodeIdValue.UnloadPoint22, new List<Valve>
                {
                    new Valve
                    {
                        Name = "Елеватор №3 Силос 5",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 8000302,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №3 Силос 5",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 8000301,
                                ExpectedResult = true
                            }
                        }
                    }
                }
            },
            {
                (long) NodeIdValue.UnloadPoint10, new List<Valve>
                {
                    new Valve
                    {
                        Name = "Елеватор №4 Силос 4 (соняшник)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 23000402,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №4 Силос 5 (соняшник)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 23000401,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 23000404,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №4 Силос 6 (соняшник)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 23000401,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 23000403,
                                ExpectedResult = true
                            }
                        }
                    }
                }
            },
            {
                (long) NodeIdValue.UnloadPoint11, new List<Valve>
                {
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 1 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 26000401,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 26000403,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 2 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 26000401,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 26000404,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 3 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 26000402,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 4 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 25000402,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 5 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 25000401,
                                ExpectedResult = true
                            }
                        }
                    }
                }
            },
            {
                (long) NodeIdValue.UnloadPoint12, new List<Valve>
                {
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 1 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 26000401,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 26000403,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 2 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 26000401,
                                ExpectedResult = true
                            },
                            new ValveDevice
                            {
                                DeviceId = 26000404,
                                ExpectedResult = true
                            }
                        }
                    },
                    new Valve
                    {
                        Name = "Елеватор №5 Силоса 3 (Соя)",
                        Devices = new List<ValveDevice>
                        {
                            new ValveDevice
                            {
                                DeviceId = 26000402,
                                ExpectedResult = true
                            }
                        }
                    }
                }
            }
        };
        
        public string GetUnloadValveByNodeId(long nodeId)
        {
            if (_valvesByNodeId.TryGetValue(nodeId, out var valves))
            {
                var selectedValves = valves
                    .Where(x => x.Devices.All(z => ((DigitalInState) Program.GetDeviceState(z.DeviceId)).InData.Value == z.ExpectedResult))
                    .ToList();

                return selectedValves.Count == 1 ? selectedValves.First().Name : string.Empty;
            }

            return null;
        }
    }
}