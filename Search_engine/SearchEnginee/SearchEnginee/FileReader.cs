using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Xml;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using word = Microsoft.Office.Interop.Word;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Presentation;
using excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text.html;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using ICSharpCode.SharpZipLib.Zip;
using Code7248.word_reader;


namespace SearchEnginee
{

    /// <summary>
    /// Class contains the Api's for reading various extensions
    /// Returns the output as a string to be added to the dictionary of the inverted index
    /// All Api's are to parse the inputs as strings
    /// The following extensions are tested (.pptx, .ppt, .xml,.doc, .docx,.html,.xlxs,.xls, .txt)
    /// No other types are allowed
    /// </summary>
    public class FileReader
    {

        public FileReader()
        {
            
        }

        public string readFile(string file)
        {
            if (Path.GetExtension(file) == ".txt")
            {
                return ReadTxt(file);
            }
            else if (Path.GetExtension(file) == ".pdf")
            {
                return ReadPdf(file);
            }
            else if (Path.GetExtension(file) == ".doc")
            {
                return ReadDoc(file);
            }
            else if (Path.GetExtension(file) == ".docx")
            {
                return ReadDocx(file);
            }
            else if (Path.GetExtension(file) == ".ppt")
            {
                return ReadPpt(file);
            }
            else if (Path.GetExtension(file) == ".ppts")
            {
                return ReadPpts(file);
            }
            else if (Path.GetExtension(file) == ".xls")
            {
                return ReadXls(file);
            }
            else if (Path.GetExtension(file) == ".xlsx")
            {
                return ReadXlsx(file);
            }
            else if (Path.GetExtension(file) == ".html")
            {
                return ReadHtml(file);
            }
            else if (Path.GetExtension(file) == ".xml")
            {
                return ReadXml(file);
            }
            else
            {
                return null;
            }
        }

        string ReadTxt(string file)
        {
            TextReader reader = new StreamReader(file);
            return (reader.ReadToEnd());
        }

        /// <summary>
        /// Loops through the pages of the pdf documents 
        /// Extracts the text from each document and stores in the variable text
        /// reader buffer must be closed to avoid data corruption
        /// Requires: The path that contains the pdf file must not be empty
        /// Ensures: The buffer reader must be closed to avoid data corruption
        /// </summary>
        /// <param name="path"></param>
        /// <returns>text</returns>
        string ReadPdf(string path)
        {

            string text = "";
            PdfReader reader;
            reader = new PdfReader(path);
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text = PdfTextExtractor.GetTextFromPage(reader, page);
                Console.WriteLine(text);
            }
            reader.Close();
            return text;
        }

        /// <summary>
        /// Uses a string builder buffer to read the text from the .doc document
        /// Creates an instance of the word document 
        /// Opens the documents using the Document keyword
        /// Counts the number of words in the documnents
        /// loops through the number of words and appends the text to the buffer
        /// convert the buffer string to immutable strings
        /// ensures: Path for reading document must not be empty
        /// requires: Ensures the document is closed to eliminate data corruption
        /// </summary>
        /// <param name="path"></param>
        /// <returns> DocText </returns>
        string ReadDoc(string path)
        {
            StringBuilder text = new StringBuilder();
            string text2;
            string DocText = text.ToString();
            word.Application application = new word.Application();
            word.Document documents = application.Documents.Open(path);
            int count = documents.Words.Count;
            for (int i = 1; i <= count; i++)
            {
                text2 = documents.Words[i].Text;
                text.Append(text2);
                Console.WriteLine(text);
            }
            application.ActiveDocument.Close();
            application.Quit();
            return DocText;
            ;
        }

        /// <summary>
        /// Uses the buffer class to read the documents in the .docx document
        /// An instance of the textextractor class is created to read the documents in the path
        /// The extracttext method is used to get the text and store in the 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>DocxText</returns>
        string ReadDocx(string path)
        {
            StringBuilder bufferText = new StringBuilder();
            string DocxText = bufferText.ToString();
            TextExtractor textextractor = new TextExtractor(path);
            string text = textextractor.ExtractText();
            bufferText.Append(text);
            Console.WriteLine(text);
            return DocxText;
        }

        /// <summary>
        /// Creates a buffer for powerpoint data to read the powerpoint slides
        /// Calls a method "GetAllTextInSlide". This method reads the .ppt documents from the path
        /// Loops through the textand appends the data to the buffer
        /// Converts the buffer to string
        /// requires: Path for powerpoint documents must not be empty
        /// ensures: a string must be returned.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>ppts</returns>
        string ReadPpt(string path)
        {
            StringBuilder pptsData = new StringBuilder();
            string ppts = pptsData.ToString();
            foreach (string data in GetAllTextInSlide(path, 1))
            {
                pptsData.Append(data);
            }

            return ppts;
        }

        /// <summary>
        /// Creates a buffer for powerpoint data to read the powerpoint slides
        /// Calls a method "GetAllTextInSlide". This method reads the .ppt documents from the path
        /// Loops through the textand appends the data to the buffer
        /// Converts the buffer to string
        /// requires: Path for powerpoint documents must not be empty
        /// ensures: a string must be returned.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadPpts(string path)
        {
            StringBuilder pptsData = new StringBuilder();
            string ppts = pptsData.ToString();

            foreach (string data in GetAllTextInSlide(path, 1))
            {
                pptsData.Append(data);
            }

            return ppts;
        }

        /// <summary>
        /// Creates a buffer using the stringbuilder class
        /// It uses the microsoft interop class to read excel files
        /// </summary>
        /// <param name="path"></param>
        /// <returns>xlsData</returns>
        string ReadXls(string path)
        {
            StringBuilder builder1 = new StringBuilder();
            string xlsData = builder1.ToString();
            excel.Application xlsApp = new excel.Application();
            excel.Workbook xlsWkb = xlsApp.Workbooks.Open(path);

            excel.Worksheet xlWorksheet = (excel.Worksheet)xlsWkb.Sheets.get_Item(1);
            excel.Range xlRange = xlWorksheet.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(
                    excel.XlRangeValueDataType.xlRangeValueDefault);

            for (int row = 1; row <= xlWorksheet.UsedRange.Rows.Count; ++row)
            {
                for (int col = 1; col <= xlWorksheet.UsedRange.Columns.Count; ++col)
                {

                    builder1.Append(valueArray[row, col].ToString());
                }
            }
            xlsWkb.Close(false);
            Marshal.ReleaseComObject(xlsWkb);
            xlsApp.Quit();
            Marshal.ReleaseComObject(xlsWkb);

            return xlsData;

        }


        /// <summary>
        /// This method reads a excel file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadXlsx(string path)
        {
            StringBuilder builder = new StringBuilder();
            string text = builder.ToString();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(CellValue))
                    {
                        builder.Append(reader.GetText());
                        Console.Write(builder + " ");
                    }
                }
                Console.WriteLine();
                Console.ReadKey();
            }
            return text;
        }

        /// <summary>
        /// This method reads an html file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadHtml(string path)
        {
            MemoryStream output = new MemoryStream();
            string htmlData = output.ToString();
            TextReader reader = new StreamReader(path);
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, output);
            HTMLWorker worker = new HTMLWorker(document);
            document.Open();
            worker.StartDocument();
            worker.Parse(reader);
            worker.EndDocument();
            worker.Close();
            document.Close();
            return htmlData;
        }

        /// <summary>
        /// This method reads an xml file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string ReadXml(string path)
        {
            XmlTextReader reader = new XmlTextReader(path);
            StringBuilder xmlstrings = new StringBuilder();
            string XmlData = xmlstrings.ToString();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        xmlstrings.Append("<" + reader.Name + ">");
                        while (reader.MoveToNextAttribute())
                            xmlstrings.Append(" " + reader.Name + "='" + reader.Value + "'" + ">");
                        Console.WriteLine(xmlstrings);
                        break;
                    case XmlNodeType.Text:
                        xmlstrings.Append(reader.Value);
                        Console.WriteLine(xmlstrings);
                        break;
                    case XmlNodeType.EndElement:
                        xmlstrings.Append("</" + reader.Name + ">");
                        Console.WriteLine(xmlstrings);
                        break;
                }
            }
            return XmlData;
        }

        private static string[] GetAllTextInSlide(string presentationFile, int slideIndex)
        {
            // Open the presentation as read-only.
            using (PresentationDocument presentationDocument = PresentationDocument.Open(presentationFile, false))
            {
                // Pass the presentation and the slide index
                // to the next GetAllTextInSlide method, and
                // then return the array of strings it returns. 
                return GetAllTextInSlide(presentationDocument, slideIndex);
            }
        }

        private static string[] GetAllTextInSlide(PresentationDocument presentationDocument, int slideIndex)
        {
            // Verify that the presentation document exists.
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            // Verify that the slide index is not out of range.
            if (slideIndex < 0)
            {
                throw new ArgumentOutOfRangeException("slideIndex");
            }

            // Get the presentation part of the presentation document.
            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation part and presentation exist.
            if (presentationPart != null && presentationPart.Presentation != null)
            {
                // Get the Presentation object from the presentation part.
                Presentation presentation = presentationPart.Presentation;

                // Verify that the slide ID list exists.
                if (presentation.SlideIdList != null)
                {
                    // Get the collection of slide IDs from the slide ID list.
                    DocumentFormat.OpenXml.OpenXmlElementList slideIds =
                        presentation.SlideIdList.ChildElements;

                    // If the slide ID is in range...
                    if (slideIndex < slideIds.Count)
                    {
                        // Get the relationship ID of the slide.
                        string slidePartRelationshipId = (slideIds[slideIndex] as SlideId).RelationshipId;

                        // Get the specified slide part from the relationship ID.
                        SlidePart slidePart =
                            (SlidePart)presentationPart.GetPartById(slidePartRelationshipId);

                        // Pass the slide part to the next method, and
                        // then return the array of strings that method
                        // returns to the previous method.
                        return GetAllTextInSlide(slidePart);
                    }
                }
            }

            // Else, return null.
            return null;
        }

        private static string[] GetAllTextInSlide(SlidePart slidePart)
        {
            // Verify that the slide part exists.
            if (slidePart == null)
            {
                throw new ArgumentNullException("slidePart");
            }

            // Create a new linked list of strings.
            LinkedList<string> texts = new LinkedList<string>();

            // If the slide exists...
            if (slidePart.Slide != null)
            {
                // Iterate through all the paragraphs in the slide.
                foreach (DocumentFormat.OpenXml.Drawing.Paragraph paragraph in
                    slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Paragraph>())
                {
                    // Create a new string builder.                    
                    StringBuilder paragraphText = new StringBuilder();

                    // Iterate through the lines of the paragraph.
                    foreach (DocumentFormat.OpenXml.Drawing.Text text in
                        paragraph.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
                    {
                        // Append each line to the previous lines.
                        paragraphText.Append(text.Text);
                    }

                    if (paragraphText.Length > 0)
                    {
                        // Add each paragraph to the linked list.
                        texts.AddLast(paragraphText.ToString());
                    }
                }
            }

            if (texts.Count > 0)
            {
                // Return an array of strings.
                return texts.ToArray();
            }
            else
            {
                return null;
            }
        }


    }
}
