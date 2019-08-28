//using System;
//using System.Collections.Generic;
//using AutomationFoundation.Runtime.Threading.Internal;
//using AutomationFoundation.Runtime.Threading.Primitives;

//namespace AutomationFoundation.Runtime.Stubs
//{
//    internal class TestableWorkerCache : WorkerCache
//    {
//        public Func<Tuple<Worker, bool>> OnGetAvailableWorkerCallback { get; set; }
//        public Func<Worker> OnCreateWorkerCallback { get; set; }

//        public bool ReusedExistingWorker { get; set; }
//        public bool CreatedWorker { get; set; }

//        public TestableWorkerCache()
//        {
//        }

//        internal TestableWorkerCache(ISet<WorkerCacheEntry> available, ISet<WorkerCacheEntry> busy)
//            : base(available, busy)
//        {
//        }

//        protected override bool TryGetAvailableWorker(out Worker worker)
//        {
//            bool result;

//            if (OnGetAvailableWorkerCallback != null)
//            {
//                var callbackResult = OnGetAvailableWorkerCallback();

//                worker = callbackResult.Item1;
//                result = callbackResult.Item2;
//            }
//            else
//            {
//                result = base.TryGetAvailableWorker(out worker);
//            }

//            ReusedExistingWorker = result;
//            return result;
//        }

//        protected override Worker CreateWorker()
//        {
//            Worker result;
//            if (OnCreateWorkerCallback != null)
//            {
//                result = OnCreateWorkerCallback();
//            }
//            else
//            {
//                result = base.CreateWorker();
//            }

//            CreatedWorker = result != null;
//            return result;
//        }
//    }
//}