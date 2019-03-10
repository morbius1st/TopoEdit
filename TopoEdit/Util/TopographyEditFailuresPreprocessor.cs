using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace TopoEdit.Util
{
    class TopographyEditFailuresPreprocessor : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            return FailureProcessingResult.Continue;
        }

    }

    class TopographyEditFailuresPreprocessorVerbose : IFailuresPreprocessor
    {
        // For debugging
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            try
            {
                TaskDialog.Show("Preprocess failures", "Hello");
                IList<FailureMessageAccessor> failureMessages = failuresAccessor.GetFailureMessages();
                int numberOfFailures = failureMessages.Count;
                TaskDialog.Show("Preprocess failures", "Found " + numberOfFailures + " failure messages.");
                if (numberOfFailures < 5)
                {
                    foreach (FailureMessageAccessor msgAccessor in failureMessages)
                    {
                        TaskDialog.Show("Failure!", msgAccessor.GetDescriptionText());
                    }
                }
                else
                {
                    TaskDialog.Show("Failure 1 of " + numberOfFailures, failureMessages.First<FailureMessageAccessor>().GetDescriptionText());
                }
                TaskDialog.Show("Preprocess failures", "Goodbye");
                return FailureProcessingResult.Continue;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Exception", e.ToString());
                return FailureProcessingResult.ProceedWithRollBack;
            }
        }

    }
}
