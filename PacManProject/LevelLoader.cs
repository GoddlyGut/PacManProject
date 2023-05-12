using Microsoft.Xna.Framework;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PacManProject
{

    enum LevelDesignElements
    {
        Wall,
        EmptySpace,
        Enemy,
        Player,
    }

    public class Level
    {
        List<string> lines = new List<string>();

        public Level(string lvlPath)
        {
            LoadLevel(lvlPath);
        }

        private void LoadLevel(string lvlPath)
        {
            using (Stream fileStream = TitleContainer.OpenStream(lvlPath))
            {
                
                string line;
                int? width = null;

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (width == null)
                        {
                            width = line.Length;
                        }
                        else if (line.Length != width)
                        {
                            throw new Exception(String.Format("The length of line {0} is different from all preceding lines.", lines.Count + 1));
                        }

                        CheckForForbiddenCharacters(line, lines.Count + 1);

                        Debug.WriteLine(line);
                        lines.Add(line);
                    }

                    if (lines.Count == 0)
                    {
                        // Handle the case when the file is empty
                        throw new Exception("The file is empty.");
                    }
                }
            }
        }

        private void CheckForForbiddenCharacters(string line, int lineNumber)
        {
            foreach (char c in line)
            {
                if (c == '.') {  }
                else if (c == '-') {  }
                else if (c == 'e' || c == 'E') {  }
                else
                {
                    throw new System.Exception(String.Format("ERROR: Value of {0} is not recognized on line(" + lineNumber +") !", c == ' ' ? "(SPACE)" : c));
                }
            }
        }

    }
}
