﻿//using SolidWorks.Interop.sldworks;
//using SolidWorks.Interop.swconst;
//using SolidWorks.Interop.swpublished;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace Du.SolidWorks.Extension
//{
//    public enum StateEnum { Insert, Edit }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <typeparam name="TMacroFeature"></typeparam>
//    /// <typeparam name="TData"></typeparam>
//    [ComVisible(false)]
//    public abstract class MacroFeatureBase<TMacroFeature,TData>:ISwComFeature
//        where TData:new ()
//        where TMacroFeature : MacroFeatureBase<TMacroFeature, TData>
//    {

//        // Store PMP in a field so the GC can't collect it
//        private IPropertyManagerPage2 _EditPage;

//        public IModelDoc2 ModelDoc { get; private set; }

//        public IFeature SwFeature { get; private set; }

//        protected IMacroFeatureData SwFeatureData { get; private set; }

//        public ISldWorks SwApp { get; private set; }

//        public TData Database { get;  set; }

//        public abstract string FeatureName { get; }

//        protected abstract swMacroFeatureOptions_e FeatureOptions { get; }

//        /// <summary>
//        /// Allows an implementation to specify what property manager page to show on edit.
//        /// </summary>
//        /// <returns></returns>
//        protected abstract IPropertyManagerPage2 GetPropertyManagerPage();

//        /// <summary>
//        /// This should be called on all callbacks defined in ISwComFeature to
//        /// initialize all the instance variables.
//        /// </summary>
//        /// <param name="app"></param>
//        /// <param name="modelDoc"></param>
//        /// <param name="feature"></param>
//        private void Init(object app, object modelDoc, object feature)
//        {
//            if (app == null) throw new ArgumentNullException(nameof(app));
//            if (modelDoc == null) throw new ArgumentNullException(nameof(modelDoc));
//            // feature can be null

//            SwApp = (ISldWorks)app;
//            if (feature != null)
//            {
//                SwFeature = (IFeature)feature;
//                SwFeatureData = (IMacroFeatureData)SwFeature.GetDefinition();
//                Database = SwFeatureData.Read<TData>();
//            }
//            else
//            {
//                Database = new TData();
//            }
//            ModelDoc = (IModelDoc2)modelDoc;
//        }

      

//        /// <summary>
//        /// Deserialize all selected objects and marks from the macro feature data. The selections
//        /// will be active. Note you have to call Commit or Cancel after calling this or the feature
//        /// manager tree will be in a rollback state.
//        /// </summary>
//        private void LoadSelections()
//        {
//            if (SwFeatureData == null) return;

//            var result = SwFeatureData.AccessSelections(ModelDoc, null);
//            if (!result)
//            {
//                throw new Exception("Can't access selections");
//            }
//        }

//        public object Edit(object app, object modelDoc, object feature)
//        {
//            //if (feature == null) throw new ArgumentNullException(nameof(feature));

//            Init(app, modelDoc, feature);
//            LoadSelections();
//            _EditPage = GetPropertyManagerPage();
//            _EditPage?.Show();
//            return true;
//        }

//        /// <summary>
//        /// Insert the macrofeature using this call
//        /// </summary>
//        /// <param name="app"></param>
//        /// <param name="modelDoc"></param>
//        public void Insert(ISldWorks app, IModelDoc2 modelDoc)
//        {
//            Edit(app, modelDoc, null);
//        }

//        public void Insert(ISldWorks app, IModelDoc2 modelDoc, TData data)
//        {
//            Init(app, modelDoc, null);
//            LoadSelections();
//            Database = data;
//            Commit();
//        }

//        public void Commit()
//        {
//            if (State == StateEnum.Insert)
//            {
//                //var editBodies = ModelDoc
//                //    .GetSelectedObjectsFromModel(Database)
//                //    .OfType<IBody2>()
//                //    .ToArray();
//                SwFeature = ModelDoc.FeatureManager.InsertMacroFeature<TMacroFeature>(FeatureName, FeatureOptions, Database, null);
//                if (SwFeature != null)
//                {
                    
//                }
//            }
//            else
//            {
//                ModifyDefinition();
//            }
//        }

//        /// <summary>
//        /// Save all selection and parameters and modify the definition of the macro feature
//        /// </summary>
//        private void ModifyDefinition()
//        {
//            SaveSelections();
//            SwFeatureData.Write(Database);
//            SwFeature.ModifyDefinition(SwFeatureData, ModelDoc, null);
//        }

//        /// <summary>
//        /// Serialize all selected objects and marks to the the macro feature data
//        /// </summary>
//        private void SaveSelections()
//        {
//            //var t = ModelDoc.GetMacroFeatureDataSelectionInfo(Database);

//            //SwFeatureData.SetSelections2(ComWangling.ObjectArrayToDispatchWrapper(t.Item1), t.Item2, t.Item3);
//            //Debug.Assert(SwFeatureData.GetSelectionCount() == t.Item1.Length);
//        }


//        private StateEnum State => SwFeatureData == null ? StateEnum.Insert : StateEnum.Edit;


//        /// <summary>
//        /// Implement to perform the security function. See sample project
//        /// </summary>
//        /// <returns></returns>
//        protected abstract object Security();
//        /// <summary>
//        /// Implement to perform the rebuild function. See sample project
//        /// </summary>
//        /// <returns></returns>
//        protected abstract object Regenerate(IModeler modeler);


//        #region ISWComFeature callbacks

//        /// <summary>
//        ///
//        /// </summary>
//        /// <param name="app"></param>
//        /// <param name="modelDoc"></param>
//        /// <param name="feature"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// Any one of the following values:
//        /// True if the rebuild is successful(independent and modify)
//        /// False if the rebuild failed
//        /// String, as displayed in an error message to the user(see Remarks)
//        /// Body, if a body was created
//        /// </remarks>
//        public object Regenerate(object app, object modelDoc, object feature)
//        {
//            Init(app, modelDoc, feature);
//            var modeller = (IModeler)SwApp.GetModeler();
//            try
//            {
//                return Regenerate(modeller);
//            }
//            catch (Exception e)
//            {
//                return e.Message;
//            }
//        }

//        public object Security(object app, object modelDoc, object feature)
//        {
//            Init(app, modelDoc, feature);
//            return Security();
//        }
//        #endregion
//    }
//}
