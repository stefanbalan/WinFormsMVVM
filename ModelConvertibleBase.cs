//	public class Delta<TStructuralType> : TypedDelta, IDelta
	//where TStructuralType : class
  
  using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace PolicyCenter.Domain
{
    public abstract class ModelConvertibleBase<TModel, TEntity> where TModel : ModelConvertibleBase<TModel, TEntity>, new()
    {
        protected ModelConvertibleBase()
        {
            Mappings = new Dictionary<string, Mapping>();
        }

        public static TModel FromEntity(TEntity entity)
        {
            var model = new TModel();
            model.FromEntityToThisModel(entity);
            return model;
        }

        protected abstract void FromEntityToThisModel(TEntity entity);

        public abstract TEntity ToEntity();

        public virtual Collection<Mapping> GetMappings()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, Mapping> Mappings { get; }

        public void AddMapping(Mapping mapping)
        {
            Mappings.Add(mapping.GetEntityProperty().Name, mapping);
        }

        public class Mapping
        {

            private Expression<Func<TEntity, object>> EntityExp { get; }
            private Expression<Func<TModel, object>> ModelExp { get; }

            public Mapping(Expression<Func<TEntity, object>> entityExp, Expression<Func<TModel, object>> modelExp)
            {
                EntityExp = entityExp;
                ModelExp = modelExp;
            }


            public PropertyInfo GetEntityProperty()
            {
                return PropertyInfo(EntityExp, typeof(TEntity));
            }

            public PropertyInfo GetModelProperty()
            {
                return PropertyInfo(ModelExp, typeof(TModel));
            }

            private static PropertyInfo PropertyInfo<T>(Expression<Func<T, object>> expression, Type type)
            {
                if (!(expression.Body is MemberExpression member))
                    throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

                var propInfo = member.Member as PropertyInfo;
                if (propInfo == null)
                    throw new ArgumentException($"Expression '{expression.Name}' refers to a field, not a property.");

                if (propInfo.ReflectedType == null ||
                    type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                    throw new ArgumentException($"Expresion '{expression.Name}' refers to a property that is not from type {type}.");
                return propInfo;
            }
        }
    }


}
