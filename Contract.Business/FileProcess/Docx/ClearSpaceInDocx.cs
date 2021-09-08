using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business
{
  public static  class ClearSpaceInDocx
    {

      public static void ClearSpaceInDocument(WordprocessingDocument fromTemplate)
      {
          SimplifyMarkupSettings settings = new SimplifyMarkupSettings
          {
              RemoveComments = true,
              RemoveContentControls = true,
              RemoveEndAndFootNotes = true,
              RemoveFieldCodes = false,
              RemoveLastRenderedPageBreak = true,
              RemovePermissions = true,
              RemoveProof = true,
              RemoveRsidInfo = true,
              RemoveSmartTags = true,
              RemoveSoftHyphens = true,
              ReplaceTabsWithSpaces = true,
          };
          MarkupSimplifier.SimplifyMarkup(fromTemplate, settings);
      }
    }
}
