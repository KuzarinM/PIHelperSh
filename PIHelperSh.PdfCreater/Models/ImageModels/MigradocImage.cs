using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreater.Models.ImageModels
{
    /// <summary>
    /// Удобный метод для хранения изображений (для создания Pdf документа можно использовать либо путь до фото, либо поток с данными о нём)
    /// </summary>
    public class MigradocImage
    {
        /// <summary>
        /// Путь до фото
        /// </summary>
        public string? Path { get; set; } = null;

        /// <summary>
        /// Поток фото
        /// </summary>
        public Stream? Source
        {
            get => _source;
            set
            {
                _source = value;
                _b64Source = null;
            }
        }

        private Stream? _source;

        private string? _b64Source = null;

        /// <summary>
        /// Создание изображения из пути
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns></returns>
        public static MigradocImage CreateFromPath(string path) => new MigradocImage { Path = path };

        /// <summary>
        /// Создания изображения из строки base64
        /// </summary>
        /// <param name="base64">изобращение в формате base64</param>
        /// <returns></returns>
        public static MigradocImage CreateFromBase64(string base64) => new MigradocImage { _b64Source = $"base64:{base64}" };

        /// <summary>
        /// Создание изображения из потока
        /// </summary>
        /// <param name="stream">Поток</param>
        /// <returns></returns>
        public static MigradocImage CreateFromStream(Stream stream) => new MigradocImage { Source = stream };

        public string GetImageForMigraDoc()
        {
            if (!string.IsNullOrEmpty(Path)) return Path;
            if (_b64Source != null) return _b64Source;
            if (_source == null) throw new ArgumentNullException("Для изображения не задан ни путь, ни поток. Необходимо заполнить как минимум одно из этого, для работы");
            _b64Source = GetImageAsStringBase64(_source);
            return _b64Source;
        }

        private string GetImageAsStringBase64(Stream stream)
        {
            //Вот это вот, в общем-то, ещё один жуткий костыль. Дело в том, что MigraDoc не поддерживает создание изображений из потоков. Только из путей. Но всё же, способ есть
            //Вот он собственно. И да, фактически это жуткая система, записывающая картинку в строку. Увы, по другому не получится
            stream.Position = 0;
            int count = (int)stream.Length;
            byte[] data = new byte[count];
            stream.Read(data, 0, count);
            return $"base64:{Convert.ToBase64String(data)}";
        }
    }
}
