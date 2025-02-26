using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hardcore
{
    internal abstract class Mod : MonoBehaviour
    {
        public string Name { get; private set; }
        public string[] DisplayInfo { get; private set; }
    }
}
