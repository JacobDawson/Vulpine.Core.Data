/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      http://www.jakesden.com/corelibrary.html
 *
 *  This file is licensed under the Apache License, Version 2.0 (the "License"); 
 *  you may not use this file except in compliance with the License. You may 
 *  obtain a copy of the License at:
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.    
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Exceptions;

namespace Vulpine.Core.Data
{
    public class Grid<E>
    {
        //contains the values for the grid
        private E[] grid;

        //stores the size of the grid
        private int num_rows;
        private int num_cols;

        /// <summary>
        /// Constructs an empty m x n grid where all the entrys are initialsied
        /// to there default values. The grid can then be built dynamicaly.
        /// </summary>
        /// <param name="rows">Number of rows in the grid</param>
        /// <param name="cols">Number of columns in the grid</param>
        /// <exception cref="ArgRangeExcp">If either the number of rows or
        /// columns is less than one</exception>
        public Grid(int rows, int cols)
        {
            //checks that the size of the grid is valid
            ArgRangeExcp.Atleast("rows", rows, 1);
            ArgRangeExcp.Atleast("cols", cols, 1);

            num_rows = rows;
            num_cols = cols;

            //creates a new default grid
            grid = new E[rows * cols];
            for (int i = 0; i < grid.Length; i++)
            grid[i] = default(E);
        }

        #region Class Properties...

        /// <summary>
        /// Represents the number of rows in the grid. Read-Only
        /// </summary>
        public int NumRows
        {
            get { return num_rows; }
        }

        /// <summary>
        /// Represents the number of columns in the grid. Read-Only
        /// </summary>
        public int NumColumns
        {
            get { return num_cols; }
        }

        /// <summary>
        /// Represents the total number of cells in the grid. Read-Only
        /// </summary>
        public int NumCells
        {
            get { return num_rows * num_cols; }
        }

        /// <summary>
        /// Acceses the values of the grid by row and column. See the
        /// SetCell() and GetCell() methods for more details.
        /// </summary>
        /// <param name="row">The row of the desired element</param>
        /// <param name="col">The column of the desired element</param>
        /// <returns>The desired element</returns>
        public E this[int row, int col]
        {
            get { return GetCell(row, col); }
            set { SetCell(row, col, value); }
        }

        #endregion //////////////////////////////////////////////////////////////

        /// <summary>
        /// Obtaines the matrix element at the given row and column.
        /// </summary>
        /// <param name="row">The row of the desired element</param>
        /// <param name="col">The column of the desired element</param>
        /// <returns>The desired element within the matrix</returns>
        /// <exception cref="ArgRangeExcp">If either the row or
        /// the column numbers lie outside the matrix</exception>
        public E GetCell(int row, int col)
        {
            //checks that the row and column are valid
            ArgRangeExcp.Check("row", row, num_rows - 1);
            ArgRangeExcp.Check("col", col, num_cols - 1);

            //obtains the desired cell value
            int index = col + (row * num_cols);
            return grid[index];
        }

        /// <summary>
        /// Sets the value at the given row and column in the matrix.
        /// </summary>
        /// <param name="row">The row of the desired element</param>
        /// <param name="col">The column of the desired element</param>
        /// <param name="value">The new value of the element</param>
        /// <exception cref="ArgRangeExcp">If either the row or
        /// the column numbers lie outside the matrix</exception>
        public void SetCell(int row, int col, E value)
        {
            //checks that the row and column are valid
            ArgRangeExcp.Check("row", row, num_rows - 1);
            ArgRangeExcp.Check("col", col, num_cols - 1);

            //sets the desired cell value
            int index = col + (row * num_cols);
            grid[index] = value;
        }

    }
}
