using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Matrixes
{
    [Serializable]
    public class Matrix : ICloneable
    {
        /// <summary>
        /// Matrix values holder
        /// </summary>
        protected double[,] _core = { };

        /// <summary>
        /// Rows ammount of matrix
        /// </summary>
        /// <returns>Unsigned int</returns>
        public uint W {
            get
            {
                return (uint)(_core.Length / H);
            }
        }

        /// <summary>
        /// Columns ammount of matrix
        /// </summary>
        /// <returns>Unsigned int</returns>
        public uint H
        {
            get
            {
                return (uint)(_core.GetUpperBound(0) + 1);
            }
        }

        /// <summary>
        /// Matrix values holder
        /// </summary>
        public double[,] Core {
            get
            {
                return _core;
            }
        }

        /// <summary>
        /// Creates new matrix based on 2D array
        /// </summary>
        /// <param name="core">2D array</param>
        public Matrix(double[,] core)
        {
            if (core.Rank != 2)
                throw new InvalidOperationException("Array rank doesn't match the matrix rank");

            _core = core.Clone() as double[,];
        }

        /// <summary>
        /// Creates new null-matrix based on rows and columns ammount
        /// </summary>
        /// <param name="height">Columns ammount</param>
        /// <param name="width">Rows ammount</param>
        public Matrix(uint height, uint width)
        {
            _core = new double[height, width];

            for (uint i = 0; i < _core.GetLength(0); i++)
            {
                for (uint j = 0; j < _core.GetLength(1); j++)
                    _core[i, j] = 0;
            }
        }

        /// <summary>
        /// Gives access to element of matrix
        /// </summary>
        /// <param name="x">Column index</param>
        /// <param name="y">Row index</param>
        /// <returns>Element in position, where x = column index and y = row index</returns>
        public double this[uint x, uint y]
        {
            get
            {
                return _core[x, y];
            }

            set
            {
                _core[x, y] = value;
            }
        }

        /// <summary>
        /// Perform summing operation for 2 matrixes. Column and row size of matrixes must be equal.
        /// </summary>
        /// <param name="leftMatrix">First matrix operand</param>
        /// <param name="rightMatrix">Second matrix operand</param>
        /// <returns>Product of adding 2 matrixes</returns>
        public static Matrix operator +(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H != rightMatrix.H || leftMatrix.W != rightMatrix.W)
                throw new InvalidOperationException("Operands sizes doesn't match!");

            Matrix result = new Matrix(leftMatrix.H, leftMatrix.W);

            for (uint i = 0; i < result.H; i++)
            {
                for (uint j = 0; j < result.W; j++)
                    result[i, j] = leftMatrix[i, j] + rightMatrix[i, j];
            }

            return result;
        }

        /// <summary>
        /// Perform subtraction operation for 2 matrixes. Column and row size of matrixes must be equal.
        /// </summary>
        /// <param name="leftMatrix">First matrix operand</param>
        /// <param name="rightMatrix">Second matrix operand</param>
        /// <returns>Product of subtraction 2 matrixes</returns>
        public static Matrix operator -(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H != rightMatrix.H || leftMatrix.W != rightMatrix.W)
                throw new InvalidOperationException("Operands sizes doesn't match!");

            Matrix result = new Matrix(leftMatrix.H, leftMatrix.W);

            for (uint i = 0; i < result.H; i++)
            {
                for (uint j = 0; j < result.W; j++)
                    result[i, j] = leftMatrix[i, j] - rightMatrix[i, j];
            }

            return result;
        }

        /// <summary>
        /// Perform multiplication operation for 2 matrixes. Columns ammout must match the row ammount.
        /// </summary>
        /// <param name="leftMatrix">First matrix operand</param>
        /// <param name="rightMatrix">Second matrix operand</param>
        /// <returns>Product of multiplication 2 matrixes</returns>
        public static Matrix operator *(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.W != rightMatrix.H)
                throw new InvalidOperationException("Rows ammount of first operand must match columns ammount of second!");

            Matrix result = new Matrix(leftMatrix.H, rightMatrix.W);

            double elementByproduct = 0;

            for (uint i = 0; i < result.H; i++)
            {
                for (uint j = 0; j < result.W; j++)
                {
                    elementByproduct = 0;

                    for (uint k = 0; k < leftMatrix.W; k++)
                        elementByproduct += leftMatrix[i, k] * rightMatrix[k, j];

                    result[i, j] = elementByproduct;
                }
            }

            return result;
        }

        /// <summary>
        /// Perform multiplication operation for matrix and scalar value => R.
        /// </summary>
        /// <param name="scalar">Scalar value => R</param>
        /// <param name="rightMatrix">Matrix Operand</param>
        /// <returns></returns>
        public static Matrix operator *(double scalar, Matrix rightMatrix)
        {
            Matrix result = new Matrix(rightMatrix.H, rightMatrix.W);

            for (uint i = 0; i < result.H; i++)
            {
                for (uint j = 0; j < result.W; j++)
                    result[i, j] = rightMatrix[i, j] * scalar;
            }

            return result;
        }

        /// <summary>
        /// Perform power raising operation. Matrix operand must be square (N = M)
        /// </summary>
        /// <param name="matrix">Matrix operand. Square only!</param>
        /// <param name="power">Power raising operand</param>
        /// <returns>Result of power raising</returns>
        public static Matrix operator ^(Matrix matrix, int power)
        {
            if (matrix.H != matrix.W)
                throw new ArgumentException("Matrix column size and row size doesn't match!");

            if (power <= 0)
                throw new ArgumentException("Wrong power argument value!");

            Matrix result = new Matrix(matrix.H, matrix.W);
            result = matrix * matrix;

            for (int i = 0; i < power - 2; i++)
                result = result * matrix;

            return result;
        }

        /// <summary>
        /// Perform memberwise comparing. Operands Columns and rows ammout must match.
        /// </summary>
        /// <param name="leftMatrix">Left operand</param>
        /// <param name="rightMatrix">Rigth operand</param>
        public static bool operator ==(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.H == rightMatrix.H && leftMatrix.W == rightMatrix.W)
            {
                for (uint i = 0; i < rightMatrix.H; i++)
                {
                    for (uint j = 0; j < rightMatrix.W; j++)
                    {
                        if (leftMatrix[i, j] != rightMatrix[i, j])
                            return false;
                    }
                }
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Perform memberwise inversed comparing. Operands Columns and rows ammout must match.
        /// </summary>
        /// <param name="leftMatrix">Left operand</param>
        /// <param name="rightMatrix">Rigth operand</param>
        public static bool operator !=(Matrix leftMatrix, Matrix rightMatrix)
        {
            return !(leftMatrix == rightMatrix);
        }

        /// <summary>
        /// Creates memberwise copy of the matrix
        /// </summary>
        /// <returns>Memberwise copy of the matrix</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [Serializable]
    public class SerializableMatrix : Matrix
    {
        public IFormatter SerializationFormatter { get; set; }
        public double[,] Serializablecore = { };

        public SerializableMatrix(double[,] core) : base(core) { Serializablecore = _core; SerializationFormatter = new BinaryFormatter(); }
        public SerializableMatrix(uint height, uint width) : base(height, width) { Serializablecore = _core; SerializationFormatter = new BinaryFormatter(); }

        public void SerializeMatrix(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                SerializationFormatter.Serialize(fs, Serializablecore);
        }

        public void DeserializeMatrix(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                double[,] _instance = (double[,])SerializationFormatter.Deserialize(fs);
                Serializablecore = _instance;
                _core = _instance;
            }
        }

        public override string ToString()
        {
            string result = "";
            result += "[";
            for (uint i = 0; i < this.H - 1; i++)
            {
                result += "[";
                for (uint j = 0; j < this.W - 1; j++)
                    result += this[i, j].ToString() + ", ";
                result += this[i, this.W - 1].ToString();
                result += "], ";
            }

            result += "[";
            for (uint j = 0; j < this.W - 1; j++)
                result += this[this.H - 1, j].ToString() + ", ";
            result += this[this.H - 1, this.W - 1].ToString();
            result += "]";

            result += "]";
            return result;
        }
    }
}
